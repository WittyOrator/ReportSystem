using System;

namespace Webb.Data
{
	public class BasketBallRules : IDisposable
	{
		public void Dispose()
		{

		}

		public static int GetPoint(string strValue)
		{
			int nPoint = 0;

//			if(strValue.Length > 0)
//			{
//				char c = strValue[0];
//
//				if(c >= '0' && c <= '9')
//				{
//					nPoint = c - '0';
//				}
//				else if(strValue.ToUpper().Equals("FT"))
//				{
//					nPoint = 1;
//				}
//			}

			switch(strValue.Trim())
			{
				case "2":
				case "2 Point":
					nPoint = 2;
					break;
				case "3":
				case "3 Point":
					nPoint = 3;
					break;
				case "FT":
					nPoint = 1;
					break;
				default:
					nPoint = 0;
					break;
			}

			return nPoint;
		}
	}
}
