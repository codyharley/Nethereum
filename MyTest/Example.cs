using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Contracts.Standards.ENS.ENSRegistry.ContractDefinition;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.Blocks;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.Eth.Transactions;
using Nethereum.RPC.Fee1559Suggestions;
using Nethereum.RPC.TransactionManagers;
using Nethereum.Web3.Accounts;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyTest
{
    [Event("Transfer")]
    public class TransferEvent_ERC20
    {
        [Parameter("address", "_from", 1, true)]
        public string From { get; set; }

        [Parameter("address", "_to", 2, true)]
        public string To { get; set; }

        [Parameter("uint256", "_value", 3, false)]
        public BigInteger Value { get; set; }

    }
    public class Example
    {
        private string mainneturl = "https://bsc.getblock.io/2ede8ab6-98b5-48c0-b7f0-22063b9e240c/mainnet/";
        private string testneturl = "https://bsc.getblock.io/2ede8ab6-98b5-48c0-b7f0-22063b9e240c/testnet/";

        // local
        //string url = "http://34.126.64.227:8545";
        //var wsurl = "ws://34.124.164.9:8546";

        //goerli
        string url = "https://goerli.infura.io/v3/2c15be96319544eda4898198a3c5d0df";
        // string wsurl = "wss://goerli.infura.io/ws/v3/2c15be96319544eda4898198a3c5d0df";


        // string url = "https://sepolia.infura.io/v3/2c15be96319544eda4898198a3c5d0df";
        //string wsurl = "wss://sepolia.infura.io/ws/v3/2c15be96319544eda4898198a3c5d0df";
        string address = "0xDb6565ce977571907D432625752d8857e3C13c26";
        Account account = new Account("2aca6af9b2bed97c59a3b79effb536c1db9749f1d379b3cc04a838e71ad0504f");
        string wordphrace = "young faculty leader glue quality guard pulse tortoise just canoe hood lecture";
        string password = "Baotram@2208";
        List<string> wallets = new List<string>() {
            "0xFC7C0d3206d110454970EaED1f41561D5bC3C21F",
            "0xDD61Ba1E8b848fbd0D47BE9C0e509AD9DE201E58",
            "0xF4EeA09DF77DDD828B243d50e4B6bFa46475DBbe"
        };
        List<string> contracts = new List<string>() {
            "0x40C0Ba4E74D9B95f2647526ee35D6E756FA8BF09",
            "0x4D864E4f542b4b40acB3151C9daD2e2C9236a88f"
        };
        public async Task TestWalletBNB()
        {
            try
            {
                account = new Account("3e10e7222821b54ec3375e7cb9a82952494673a8f61cb2feaf2703bc078a9a33", 97);
                var web3 = new Nethereum.Web3.Web3(account, testneturl);
                web3.TransactionManager.UseLegacyAsDefault = true;
                var ballance = await web3.Eth.GetBalance.SendRequestAsync("0x8041F92024923D6c79b05d57FA82e16499EaD66f");
                var currentbalance = Nethereum.Util.UnitConversion.Convert.FromWei(ballance);
                if (currentbalance > 0)
                {
                    var transactionManager = web3.TransactionManager;
                    var fromAddress = transactionManager?.Account?.Address;
                    //Sending transaction
                    var transactionInput = EtherTransferTransactionInputBuilder.CreateTransactionInput(fromAddress, "0xdf4A3155C7D9517ed552b73Eef7808509b5F66BB", 0.01m, 2);
                    //Raw transaction signed
                    var rawTransaction = await transactionManager.SignTransactionAsync(transactionInput);
                    var txnHash = await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync("0x" + rawTransaction);
                    Console.WriteLine(txnHash);
                    var txnReceipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(txnHash);

                    while (txnReceipt == null)
                    {
                        Thread.Sleep(1000);
                        txnReceipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(txnHash);
                    }


                    //var chainid = await web3.Eth.ChainId.SendRequestAsync();

                    //var feeStrategy = new SimpleFeeSuggestionStrategy(web3.Client);

                    //var fee = await feeStrategy.SuggestFeeAsync();
                    //var trans = new TransactionInput()
                    //{
                    //    ChainId = new HexBigInteger(chainid),
                    //    Type = new HexBigInteger(2),
                    //    From = web3.TransactionManager.Account.Address,
                    //    MaxFeePerGas = new HexBigInteger(fee.MaxFeePerGas.Value),
                    //    MaxPriorityFeePerGas = new HexBigInteger(fee.MaxPriorityFeePerGas.Value),
                    //    Nonce = await web3.Eth.TransactionManager.Account.NonceService.GetNextNonceAsync(),
                    //    To = "0xdf4A3155C7D9517ed552b73Eef7808509b5F66BB",
                    //    Value = new HexBigInteger(new BigInteger(0.01M))

                    //};
                    //var encoded = await web3.TransactionManager.SignTransactionAsync(trans);

                    ////  web3.TransactionManager.ra
                    //var txId = await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync("0x" + encoded);
                    //var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(txId);

                    //while (receipt == null)
                    //{
                    //    Thread.Sleep(1000);
                    //    receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(txId);
                    //}
                }
            }
            catch (Exception ex)
            {

            }

        }
        public async Task TestHDWalletAsync()
        {
            //Nethereum.HdWallet.Wallet wallet = new Nethereum.HdWallet.Wallet(NBitcoin.Wordlist.English,NBitcoin.WordCount.Twelve,"Baotram@2208");

            // string[] wordlist = wallet.Words;
            //string wordliststr = string.Join(' ', wordlist);

            Nethereum.HdWallet.Wallet wallet2 = new Nethereum.HdWallet.Wallet(wordphrace, password);
            //var publicWallet = wallet2.GetMasterPublicWallet();


            string[] addresslist = wallet2.GetAddresses(10);

            var account1 = wallet2.GetAccount(address);

            var web3 = new Nethereum.Web3.Web3(account1, url);

            var ballance1 = await web3.Eth.GetBalance.SendRequestAsync(address);

            var currentbalance1 = Nethereum.Util.UnitConversion.Convert.FromWei(ballance1);

            var defaultaddress = wallet2.GetMasterPublicWallet().ExtPubKey.ToString();



        }

        public async Task Deposit()
        {
            var web3 = new Nethereum.Web3.Web3(account, url);
            long blocknumber = 15764289;
            // example from wallet
            foreach (var item in wallets)
            {
                // check balance
                var ballance = await web3.Eth.GetBalance.SendRequestAsync(item);
                var currentbalance = Nethereum.Util.UnitConversion.Convert.FromWei(ballance);

                Console.WriteLine(currentbalance);

                // check balance in account
                var balances = await web3.Eth.ERC20.GetAllTokenBalancesUsingMultiCallAsync(item, contracts, 10000, "0xcA11bde05977b3631167028862bE2a173976CA11");

                if (balances != null && balances.Count() > 0)
                {
                    foreach (var bal in balances)
                    {
                        Console.WriteLine(Nethereum.Util.UnitConversion.Convert.FromWei(bal.Balance));
                    }
                }
                //    var filterTo = new FilterInputBuilder<TransferEvent_ERC20>()
                //    .AddTopic(tfr => tfr.To, item)
                //  .Build();
                var ethGetTransactionCount = new EthGetTransactionCount(web3.Client);
                var ethGetTransactionByBlockNumberAndIndex = new EthGetTransactionByBlockNumberAndIndex(web3.Client);
                var transnumber = await ethGetTransactionCount.SendRequestAsync(item).ConfigureAwait(false);

                //xba contract
                string contractAddress = "0x4D864E4f542b4b40acB3151C9daD2e2C9236a88f";


                var transferEventHandler = web3.Eth.GetEvent<TransferEventDTO>(contractAddress);

                var transferEventHandlerAnyContract = web3.Eth.GetEvent<TransferEventDTO>();
                var filterTransferEventsForContractReceiverAddress2 = transferEventHandler.CreateFilterInput("000000000000000000000000a3bbfe56f655791b039fa84ddaff713ee9344941", "00000000000000000000000000000000000000000000000000000000000f4240");


                var transferEventsForContractReceiverAddress2 = await transferEventHandler.GetAllChangesAsync(filterTransferEventsForContractReceiverAddress2);



            }
            // var filter = new FilterInputBuilder<TransferEvent_ERC20>().Build();

            Console.Read();
        }

        public async Task TestWallet()
        {

            var web3 = new Nethereum.Web3.Web3(account, url);
            var ballance = await web3.Eth.GetBalance.SendRequestAsync("0xB6299e2c77B5fC1992Fc31c24548074edebb5b0A");
            var currentbalance = Nethereum.Util.UnitConversion.Convert.FromWei(ballance);
            Console.WriteLine($"0xB6299e2c77B5fC1992Fc31c24548074edebb5b0A : {currentbalance} BNB");
            // get all balance for token
            var allbalances = await web3.Eth.ERC20.GetAllTokenBalancesUsingMultiCallAsync("0xdc3a7d0f0392e0b97ed84b6bdf597cd07edb4c30"
                , new List<string>() {
           "0x4D864E4f542b4b40acB3151C9daD2e2C9236a88f",
           "0x40C0Ba4E74D9B95f2647526ee35D6E756FA8BF09"

                }, 3, "0xcA11bde05977b3631167028862bE2a173976CA11");

            //xba contract
            string contractAddress = "0x4D864E4f542b4b40acB3151C9daD2e2C9236a88f";

            var contractsv = web3.Eth.ERC20.GetContractService(contractAddress);

            var transferEventHandler = web3.Eth.GetEvent<TransferEventDTO>(contractAddress);


            var ethBlockNumber = new EthBlockNumber(web3.Client);
            var currentblocknumber = await ethBlockNumber.SendRequestAsync().ConfigureAwait(false);
            var latestBlockNumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
            //currentblocknumber = latestBlockNumber;

            //25593392

            for (var i = 25593392; i <= latestBlockNumber.Value; i++)
            {
                var blockfrom = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync(i);
                var trans = await web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(blockfrom);
                foreach (var item in trans.Transactions)
                {
                    var tranitem = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(item.TransactionHash);

                    var transactionwithstatus = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(item.TransactionHash);

                    string toaddress = item.To;
                }
            }


            //var filterAllTransferEventsForContract = transferEventHandler.CreateFilterInput(fromBlock:new Nethereum.RPC.Eth.DTOs.BlockParameter() { BlockNumber = new Nethereum.Hex.HexTypes.HexBigInteger(new BigInter(25593335))});
            //var allTransferEventsForContract = await transferEventHandler.GetAllChangesAsync(filterAllTransferEventsForContract);

            var transferEventHandlerAnyContract = web3.Eth.GetEvent<TransferEventDTO>();
            var filterTransferEventsForContractReceiverAddress2 = transferEventHandler.CreateFilterInput(account.Address);
            var transferEventsForContractReceiverAddress2 = await transferEventHandler.GetAllChangesAsync(filterTransferEventsForContractReceiverAddress2);

            // var filterTransferEventsForAllContractsReceiverAddress2 = transferEventHandlerAnyContract.CreateFilterInput(account.Address, new[] { "0x16" });
            //var results = await transferEventHandlerAnyContract.GetAllChangesAsync(filterTransferEventsForAllContractsReceiverAddress2);

        }

        public async Task TestSubcription()
        {
            // test subcriions log
            //var example = new LogsERC20Subscriptions(wsurl);
            // await example.SubscribeAndRunAsync();

        }

    }
}
