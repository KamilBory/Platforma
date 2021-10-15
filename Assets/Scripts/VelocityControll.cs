using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace VelocityControll
{


    public class VelCheck
    {
        double v = 0;
        double x = 0;
        double alpha;
        Task t;
        TaskCompletionSource<bool> sending, pause_task, resume_task;
        CancellationTokenSource clT;
        SerialPort serialPort;
        public double getX()
        {
            return x;
        }
        public double getV()
        {
            return v;
        }
        public bool init(string port, int baudRate = 115200)
        {
            try
            {
                clT = new CancellationTokenSource();
                t = null;
                serialPort = new SerialPort(port, baudRate);
                serialPort.Handshake = Handshake.None;
                serialPort.Open();
                return true;
            }
            catch (Exception e)
            {
                //Debug.Log("Exception: {0}", e);
            }
            return false;

        }
        public bool measurement_start()
        {
            try
            {
                serialPort.DataReceived += (sender, e) => get_data(sender, e, serialPort);

                t = new Task(() => update(clT.Token));
                t.Start();
                return true;
            }
            catch (Exception e)
            {
                //Console.WriteLine("Exception: {0}", e);
                Debug.Log("Elko");
            }
            return false;
        }
        /*public void stop()
        {
            
        }*/
        bool pause_flag = false;
        async void update(CancellationToken tk)
        {


            try
            {
                while (!tk.IsCancellationRequested)
                {
                    if (pause_flag)
                    {
                        pause_task.SetResult(true);
                        await resume_task.Task;
                    }
                    sending = new TaskCompletionSource<bool>();
                    serialPort.Write("*A0s4d0 *Z");
                    await sending.Task;
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine("Exception: {0}", e);
            }
            serialPort.Close();
            //tk.ThrowIfCancellationRequested();
        }
        public void exit()
        {
            serialPort.Close();
            clT.Cancel();
            clT.Dispose();
        }
        public void setV(double V, double a, int k = 0)
        {
            pause();
            serialPort.Write(String.Format("*A2s4d0 *I0:{0}*I1:{1}*I2:{2}*Z", V, k, a));
            resume();
        }
        public void setAlpha(double alpha)
        {
            pause();
            serialPort.Write(String.Format("*A3s4d0 *I0:{0}*Z", alpha / 0.565));
            resume();
        }
        async public void getAlpha()
        {
            pause();
            serialPort.Write("*A1s4d0 *Z");
            sending = new TaskCompletionSource<bool>();
            await sending.Task;
            resume();
        }
        public async void pause()
        {
            if (t != null)
            {
                pause_task = new TaskCompletionSource<bool>();

                pause_flag = true;
                resume_task = new TaskCompletionSource<bool>();
                await pause_task.Task;
            }

        }
        public async void resume()
        {
            if (t != null)
            {
                pause_flag = false;
                resume_task.SetResult(true);
            }
        }
        void get_data(object sender, SerialDataReceivedEventArgs e, SerialPort port)
        {
            byte[] data = new byte[port.BytesToRead];
            port.Read(data, 0, data.Length);

            string value = System.Text.Encoding.UTF8.GetString(data);

            if (value.Length > 6)
            {
                if (value.Substring(0, 6) == "*A1s4*")
                {
                    Regex rx = new Regex("\\*O1:(\\d+\\.\\d+)\\*");
                    var Mt = rx.Match(value).Groups;
                    if (Mt.Count > 0) alpha = Convert.ToDouble(Mt[1].Value);
                }
                if (value.Substring(0, 6) == "*A0s4*")
                {
                    Regex rx = new Regex("\\*O1:(\\d+\\.\\d+)\\*");

                    try
                    {
                        var Mt = rx.Match(value).Groups;
                        if (Mt.Count > 0) v = Convert.ToDouble(Mt[1].Value);
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("{0}\n{1}", ex, rx.Match(value).Groups[1].Value);
                    }

                }
            }

            sending.SetResult(true);
        }
    }
}
