// See https://aka.ms/new-console-template for more information
using MyTest.Socket;
using Nethereum.Contracts.Standards.ENS.ENSRegistry.ContractDefinition;
using Nethereum.RPC.Eth.Blocks;
using Nethereum.Web3.Accounts;

Console.WriteLine("Hello, World!");
bool running = true;

while (running)
{



    // Console.Read();
    // can mo personal
    //var listaccount = await  web3.Personal.ListAccounts.SendRequestAsync();
    //var account = await web3.Personal.NewAccount.SendRequestAsync("Baotram@2208");
    //var unlockedaccount = await web3.Personal.UnlockAccount.SendRequestAsync("", "", 1000);

    MyTest.Example example = new MyTest.Example();

    // await example.TestWalletBNB();
    await example.TestBNB();
    //await example.Deposit();

    Console.WriteLine("Date:" + DateTime.Now.ToString());
}
