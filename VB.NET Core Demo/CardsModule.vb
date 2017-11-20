Imports Cards

Module CardsModule
    Sub Main()
        'Create reader settings
        Dim readerSettings As New ReaderSettings() With {
            .DeviceName = "ACS - ACR122U PICC Interface"
        }

        'Create reader credentials
        'This API key is provided by Cards
        Dim readerCredentials As New ReaderCredentials() With {
            .ApiKey = "ABCDE1234ABCDE1234ABCDE1234"
        }

        'Create a card reader
        Dim cardReader As New CardReader(readerSettings, readerCredentials)

        'Subscribe to Card Tap event
        AddHandler cardReader.OnCardTap, AddressOf HandleCardTap

        'Subscribe to Status Change event
        AddHandler cardReader.OnStatusChange, AddressOf HandleStatusChange

        'Start listening!
        cardReader.Listen()
    End Sub

    ''' <summary>
    ''' Handle Card Tap event. This event is raised when a user taps his phone on the reader. 
    ''' Note that Cards must be installed on the phone.
    ''' </summary>
    Sub HandleCardTap(cardInfo As CardTapResponse)
        If Not cardInfo.IsSuccess Then
            Throw New Exception(String.Format("Failed reading card, error: [{0}] {1}", CInt(cardInfo.Error), cardInfo.Error.ToString()))
        End If

        Console.WriteLine(String.Format("Card read, user ID: '{0}'.", cardInfo.CardDetails.UserID))

        'Your code goes here!
        'Do whatever you want with the accepted User ID!

        '-----------------------
        'Example: Open the door, if the user is authorized
        '-----------------------
        'if (YourSystem.IsAuthorizedToOpenDoor(cardInfo.CardDetails.UserID, Doors.Hallway))
        '{
        '   YourSystem.OpenDoor(Doors.Hallway);
        '}
        '            


        '-----------------------
        'Example: Remove balance
        '-----------------------
        '
        'YourSystem.Users.ChangeBalance(cardInfo.CardDetails.UserID, -10);
        '            

    End Sub

    ''' <summary>
    ''' Handles reader status change. 
    ''' </summary>
    Sub HandleStatusChange(readerStatus As ReaderStatus)
        Select Case readerStatus
            Case readerStatus.Disconnected
                Console.WriteLine("Card reader has been disconnected!")
                Exit Select
            Case readerStatus.Connected
                Console.WriteLine("Card reader has been connected!")
                Exit Select
        End Select
    End Sub
End Module
