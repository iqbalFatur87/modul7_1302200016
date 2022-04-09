using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace modul7_1302200016
{
    internal class BankTransferConfig
    {
        public string Language { get; set; }
        public int TransferThreshold { get; set; }
        public int TransferLowFee { get; set; }
        public int TransferHighFee { get; set; }
        public string Confirmations { get; set; }

		public List<string> Methods { get; set; }
        public BankTransferConfig()
        {
			Methods = new List<string>();
			ReadFile();
		}

		//INPUT
		public void Transfer()
		{
			if (Language == "en")
			{
				Console.WriteLine("please insert the amount of money to transfer");
			}
			else
			{
				Console.WriteLine("masukkan jumlah uang yang akan di-transfer:");
			}
			string rawTransfer = Console.ReadLine();
			int transfer = int.Parse(rawTransfer);
			int biayaTransfer;

			int totalBiaya;

			if (transfer <= TransferThreshold)
			{
				biayaTransfer = TransferLowFee;
			}
			else
			{
				biayaTransfer = TransferHighFee;
			}

			totalBiaya = transfer + biayaTransfer;

			if (Language == "en")
			{
				Console.WriteLine($"transfer fee: {biayaTransfer}");
				Console.WriteLine($"total amount: {totalBiaya}");

				Console.WriteLine("\nselect transfer method: ");
			}
			else
			{
				Console.WriteLine($"biaya transfer: {biayaTransfer}");
				Console.WriteLine($"total biaya: {totalBiaya}");

				Console.WriteLine("\npilih metode transfer: ");
			}

			for (int i = 0; i < Methods.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {Methods[i]}");
			}
			string method = Console.ReadLine();

			if (Language == "en")
			{
				Console.WriteLine($"type this {Confirmations}");
			}
			else
			{
				Console.WriteLine($"ketik {Confirmations}");
			}
			string confirm = Console.ReadLine();

			if (confirm == Confirmations)
			{
				if (Language == "en")
				{
					Console.WriteLine("transfer is completed");
				}
				else
				{
					Console.WriteLine("proses transfer berhasil");
				}
			}
			else
			{
				if (Language == "en")
				{
					Console.WriteLine("transfer is cancelled");
				}
				else
				{
					Console.WriteLine("transfer dibatalkan");
				}
			}

		}

		// GET FILE PATH
		private string GetFilePath => Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "bank_transfer_config.json");
		
		// READ JSON FILE
        public void ReadFile()
        {
            var file = File.OpenText(GetFilePath);

            JsonElement json = JsonSerializer.Deserialize<JsonElement>(file.ReadToEnd());

            Language = json.GetProperty("lang").GetString();

            TransferThreshold = json.GetProperty("transfer").GetProperty("threshold").GetInt32();
            TransferLowFee = json.GetProperty("transfer").GetProperty("low_fee").GetInt32();
            TransferHighFee = json.GetProperty("transfer").GetProperty("high_fee").GetInt32();

            Confirmations = json.GetProperty("confirmation").GetProperty(Language).GetString();

            foreach (var item in json.GetProperty("methods").EnumerateArray().ToList())
            {
                Methods.Add(item.GetString());
            }
        }
    }
}
