using TCPLibrary;

// You can also pass the server address and port number as arguments
// TcpServerClass MyTcpServer = new TcpServerClass("127.0.0.1.", 13000);

TcpServerClass tcpServer = new TcpServerClass();
await tcpServer.StartAsync();