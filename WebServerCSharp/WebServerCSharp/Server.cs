using System.Net;
using System.Net.Sockets;
namespace WebServerCSharp
{
    public class Server
    {
        public EndPoint Ip; // представляет ip-адрес
        public int Listen; // представляет наш port
        public bool Active; // представляет состояние сервера, работает он(true) или нет(false)
        private Socket _listener; // представляет объект, который ведет прослушивание
        private volatile CancellationTokenSource _cts; // токен отменты, с помощью него будут останавливаться потоки при остановке сервера
        
        public Server(string ip, int port)
        {
            this.Listen = port;
            this.Ip = new IPEndPoint(IPAddress.Parse(ip), Listen);
            this._listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._cts = new CancellationTokenSource();
            
        }
        
        public void Start()
        {
            if (!Active)
            {
                _listener.Bind(Ip);
                _listener.Listen(16);
                Active = true;
                while (Active || !_cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        Socket listenerAccept = _listener.Accept();
                        if (listenerAccept != null)
                        {
                            Task.Run(() => ClientThread(listenerAccept),_cts.Token);
                        }
                    }
                    catch { }
                }
            }
            else
            {
                Console.WriteLine("Server was started");
            }
        }

        public void Stop()
        {
            if (Active)
            {
                _cts.Cancel();
                _listener.Close();
                Active = false;
            }
            else
            {
                Console.WriteLine("Server was stopped");
            }
        }

        public void ClientThread(Socket client)
        {
            new Client(client);
        }
    }
}
