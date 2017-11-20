using System;

namespace Cards.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create reader settings
            ReaderSettings readerSettings = new ReaderSettings()
            {
                DeviceName = "ACS - ACR122U PICC Interface"
            };

            //Create reader credentials
            ReaderCredentials readerCredentials = new ReaderCredentials()
            {
                ApiKey = "ABCDE1234ABCDE1234ABCDE1234" //This API key is provided by Cards
            };

            //Create a card reader
            CardReader cardReader = new CardReader(readerSettings, readerCredentials);

            //Subscribe to Card Tap event
            cardReader.OnCardTap += HandleCardTap;

            //Subscribe to Status Change event
            cardReader.OnStatusChange += HandleStatusChange;

            //Start listening!
            cardReader.Listen();
        }

        /// <summary>
        /// Handle Card Tap event. This event is raised when a user taps his phone on the reader. 
        /// Note that Cards must be installed on the phone.
        /// </summary>
        public static void HandleCardTap(CardTapResponse cardInfo)
        {
            if (!cardInfo.IsSuccess)
            {
                throw new Exception(string.Format("Failed reading card, error: [{0}] {1}", (int)cardInfo.Error, cardInfo.Error.ToString()));
            }

            Console.WriteLine(string.Format("Card read, user ID: '{0}'.", cardInfo.CardDetails.UserID));

            //Your code goes here!
            //Do whatever you want with the accepted User ID!

            //-----------------------
            //Example: Open the door, if the user is authorized
            //-----------------------
            /*if (YourSystem.IsAuthorizedToOpenDoor(cardInfo.CardDetails.UserID, Doors.Hallway))
            {
                YourSystem.OpenDoor(Doors.Hallway);
            }
            */

            //-----------------------
            //Example: Remove balance
            //-----------------------
            /*
                YourSystem.Users.ChangeBalance(cardInfo.CardDetails.UserID, -10);
            */
        }

        /// <summary>
        /// Handles reader status change. 
        /// </summary>
        public static void HandleStatusChange(ReaderStatus readerStatus)
        {
            switch (readerStatus)
            {
                case ReaderStatus.Disconnected:
                    Console.WriteLine("Card reader has been disconnected!");
                    break;
                case ReaderStatus.Connected:
                    Console.WriteLine("Card reader has been connected!");
                    break;
            }
        }
    }
}
