using TCPLibrary;

// You can aslo pass the the server address and port number as arguments
// TcpClientClass MyTcpClient = new TcpClientClass("127.0.0.1.", 13000);

TcpClientClassJson MyTcpClient = new TcpClientClassJson();
await MyTcpClient.StartAsync();