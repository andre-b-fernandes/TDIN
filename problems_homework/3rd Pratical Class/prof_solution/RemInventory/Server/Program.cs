﻿using System;
using System.Runtime.Remoting;

namespace Server {
  class Program {
    static void Main(string[] args) {
      RemotingConfiguration.Configure("Server.exe.config", false);
      Console.WriteLine("[Server] hosting InventoryManager");
      Console.WriteLine("Press Enter to exit");
      Console.ReadLine();
    }
  }
}
