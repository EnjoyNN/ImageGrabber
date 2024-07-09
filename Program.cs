using System;
using System.Net;
using GetRequestNETStandart;
using System.Media;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;

namespace image_grab
{
	//у имгбб и за час ниего не нашло, у постимн две за час. имгур одну в секунд 20 выдает
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hellooooooooooooooooooo !!");
			Program p = new Program();
			p.start();

		}
		//GetRequestt browser = new GetRequestt();
		byte[] emptyImageimgur = File.ReadAllBytes("example-empty.dat");

		private void start()
		{
			//imgur(1);
			var threads = new List<Thread>();
			for (int i = 0; i < 5000000; i++)
			{

				if (threads.Count == 50)
				{
					foreach (var tthread in threads) tthread.Start();
					foreach (var tthread in threads) tthread.Join();
					threads.Clear();
				}

				int normali = i;
				var thread = new Thread(() =>
				{
					try
					{
						//postimg(normali);
						//imgbb(normali);
						imgur(normali);
					}
					catch (Exception ex)
					{
						//	throw;
					}
				});
				threads.Add(thread);
			}

			foreach (var tthread in threads) tthread.Start();
			foreach (var tthread in threads) tthread.Join();
		}


		private void imgbb(int normali)
		{
			Console.WriteLine("imgbb " + normali);
			var browser = new GetRequestt();
			string random = getRandomString(7);
			//random = "PwDm0bn";

			browser.Get("https://ibb.co/" + random);
			reconnectBrowser(browser, "https://ibb.co/" + random);

			if (browser.Document.Contains("<meta property=\"og:image\""))
			{

				if (OperatingSystem.IsWindows() == true)
				{
					//SoundPlayer dfsdf = new SoundPlayer();
					SoundPlayer sp = new SoundPlayer();
					sp.SoundLocation = "sound.wav";
					//sp.SoundLocation = Environment.CurrentDirectory + "/sound.wav";
					sp.Play();

				}

				string url = browser.Document.Substring("<meta property=\"og:image\" content=\"", "\"");

				string format = url.Split(".")[url.Split(".").Length - 1];

				using (WebClient client = new WebClient())
				{
					client.DownloadFile(new Uri(url), "image/" + random + "." + format);
				}

				//помоему клиентом быстрее, но это так на глаз совсем
				//browser.DownloadFile(url, "image/" + random + "1." + format);

			}
		}

		private void postimg(int normali)
		{
			Console.WriteLine("postimg " + normali);
			var browser = new GetRequestt();
			string random = getRandomString(8);
			//random = "yWhT5Vw5";


			browser.Get("https://postimg.cc/" + random);
			reconnectBrowser(browser, "https://postimg.cc/" + random);

			if (!browser.Document.Contains("<title>Error 404 (Not found)</title>"))
			{

				if (OperatingSystem.IsWindows() == true)
				{
					//SoundPlayer dfsdf = new SoundPlayer();
					SoundPlayer sp = new SoundPlayer();
					sp.SoundLocation = "sound.wav";
					//sp.SoundLocation = Environment.CurrentDirectory + "/sound.wav";
					sp.Play();

				}

				string url = browser.Document.Substring("<meta property=\"og:image\" content=\"", "\"");

				string format = url.Split(".")[url.Split(".").Length - 1];

				using (WebClient client = new WebClient())
				{
					client.DownloadFile(new Uri(url), "image/" + random + "." + format);
				}

				//помоему клиентом быстрее, но это так на глаз совсем
				//browser.DownloadFile(url, "image/" + random + "1." + format);

			}
		}

		private void imgur(int normali)
		{
			Console.WriteLine("imgur " + normali);
			var browser = new GetRequestt();
			string random = getRandomString(7);
			//random = "lpWtvT9";

			//с одной ссылки вроде работает и jpeg и jpg и png. поэтому формат ставлю один
			//даже гифку грузит по jpg, поэтому думаю формат менять не надо, только проверку убрать для гарантии
			//browser.Get("https://i.imgur.com/" + random + ".jpg");
			//reconnectBrowser(browser, "https://i.imgur.com/" + random + ".jpg");

			/*using (WebClient client = new WebClient())
			{
				client.DownloadFile(new Uri("https://i.imgur.com/" + random + ".jpg"), "image/" + random + ".jpg");
			}
			Image ff = Image.
			Image img = Image.FromFile("image/" + random + ".jpg");
			if (img.Height == 81 && img.Width == 161 && img.Flags == 73744)
				File.Delete("image/" + random + ".jpg");*/

			WebClient wc = new WebClient();


			/*using (BinaryReader br = new BinaryReader(f))
			{
				imageBytes = br.ReadBytes(500000);
				br.Close();
			}*/


			byte[] data = wc.DownloadData("https://i.imgur.com/" + random + ".jpg");

			if (!StructuralComparisons.StructuralEqualityComparer.Equals(data, emptyImageimgur))
			{
				if (OperatingSystem.IsWindows() == true)
				{
					SoundPlayer sp = new SoundPlayer();
					sp.SoundLocation = "sound.wav";
					sp.Play();

				}

				using (MemoryStream mem = new MemoryStream(data))
				{
					using (var yourImage = Image.FromStream(mem))
					{
						yourImage.Save("image/" + random + ".jpg", ImageFormat.Jpeg);
					}
				}
			}









			/*if (!browser.Document.Contains("PNG"))
			{

				if (OperatingSystem.IsWindows() == true)
				{
					//SoundPlayer dfsdf = new SoundPlayer();
					SoundPlayer sp = new SoundPlayer();
					sp.SoundLocation = "sound.wav";
					//sp.SoundLocation = Environment.CurrentDirectory + "/sound.wav";
					sp.Play();

				}

				string url = "https://i.imgur.com/" + random + ".jpg";

				GetRequestt br = new GetRequestt();
				br.DownloadFile(url, "image/" + random + ".jpg");

				using (WebClient client = new WebClient())
				{
					client.DownloadFile(new Uri(url), "image/" + random + ".jpg");
				}


				int ff = 5;
				//здесь проблема что мы грузим дважды одну картинку, чтобы проверять и отсеивать removed, но это потом надо будет как то разобраться. можно ли проверять client при загрузке и смотреть что в нем,
				//или сам файл смотреть как то и удалять. сравнивать, например с пустым.
				//и помом посмотреть как другие реализованы быстрые, и в целом поиграть с мультитредингом можно

			}*/

		}


		private string getRandomString(int countSymbols)
		{
			string result = "";
			string alphabet = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789";

			Random rnd = new Random();
			for (int i = 0; i < countSymbols; i++)
			{
				result += alphabet[rnd.Next(0, alphabet.Length)];
			}

			return result;
		}

		private void reconnectBrowser(GetRequestt browser, string url)
		{
			int countConnect = 0;
			while (browser.Document == "")
			{
				if (countConnect == 15)
				{
					break;
				}
				browser.Get(url);
				countConnect++;
			}
		}

	}

}
