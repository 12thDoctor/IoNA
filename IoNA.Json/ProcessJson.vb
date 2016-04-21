Imports Newtonsoft.Json

Public Class ProcessJson



    Private Shared CsvItemCollection As New Queue(Of CsvItemClass)
    Shared CWriter As CsvHelper.CsvWriter

    Shared TWriter As IO.TextWriter

    Shared Property RootObjects() As Rootobject()



    Public Shared Sub Main()

        Call LoadJson()

        Dim totalCount As Integer = 0

        For Each ro In RootObjects.SelectMany(Function(r) r.data.children)
            totalCount += 1

            ' Verify that the item is an actual pdf file and not a false-positive search result
            If ro.data.url.EndsWith(".pdf") Then CsvItemCollection.Enqueue(GetCsvItemClass(ro))
        Next

        RootObjects = Nothing       ' Set RootObjects to nothing, now that JSON class objects aren't needed

        Debug.Print($"{CsvItemCollection.Count} PDFS out of {totalCount} Results")

        Call InitializeSheet()      ' All json objects have been loaded and stored in memory; initialize the output file and stream 


        While CsvItemCollection.Count > 0       ' Remove an item from the CsvItem Collection and write it to the Csv Output until the Queue is empty
            CWriter.WriteRecord(Of CsvItemClass)(CsvItemCollection.Dequeue)
        End While

        TWriter.Flush()             ' Write stream to file


        CWriter.Dispose() : CWriter = Nothing        ' Cleanup
        TWriter.Dispose() : TWriter = Nothing

        Beep()                      ' because fuck you, that's why :) 


    End Sub


    ''' <summary>
    ''' Load the Json files, deserialize, and combine
    ''' </summary>
    Private Shared Sub LoadJson()

        Dim JsonRaw As String = Nothing
        Dim InputJsonFilePath As String = Nothing

        ' First Json File
        InputJsonFilePath = "C:\$Development\-Side Projects\IoNA\JSON\search.json"

        Using StrReader As New IO.StreamReader(InputJsonFilePath)
            JsonRaw = StrReader.ReadToEnd
        End Using

        Dim ro1 = JsonConvert.DeserializeObject(Of Rootobject)(JsonRaw)

        ' Second Json File
        InputJsonFilePath = "C:\$Development\-Side Projects\IoNA\JSON\search (1).json"

        Using StrReader As New IO.StreamReader(InputJsonFilePath)
            JsonRaw = StrReader.ReadToEnd
        End Using

        Dim ro2 = JsonConvert.DeserializeObject(Of Rootobject)(JsonRaw)

        ' Third Json File
        InputJsonFilePath = "C:\$Development\-Side Projects\IoNA\JSON\search (2).json"
        Using StrReader As New IO.StreamReader(InputJsonFilePath)
            JsonRaw = StrReader.ReadToEnd
        End Using

        Dim ro3 = JsonConvert.DeserializeObject(Of Rootobject)(JsonRaw)

        ' Combine the RootObjet outputs
        RootObjects = {ro1, ro2, ro3}



    End Sub


    ''' <summary>
    ''' Converts a submission json item to the smaller, CsvItemClass that will be used for outputting
    ''' </summary>
    ''' <param name="s">Json Submission Class</param>
    ''' <returns>Output CsvItemClass</returns>
    Private Shared Function GetCsvItemClass(s As Submission) As CsvItemClass
        Dim CItem As New CsvItemClass

        CItem.ItemNo = (CsvItemCollection.Count + 1)
        CItem.LinkDomain = s.data.domain

        If s.data.link_flair_text?.Trim.Length > 0 Then
            ' Strip the brackets from the flair text and prepend the /r/ for the subreddit
            CItem.SourceSubreddit = $"/r/{s.data.link_flair_text.Replace("[", vbNullString).Replace("]", vbNullString)}"
        End If

        CItem.LinkTitle = s.data.title
        CItem.LinkURL = s.data.url
        CItem.Permalink = s.data.permalink.Replace("?ref=search_posts", vbNullString)
        CItem.Score = s.data.score
        CItem.Subreddit = $"/r/{s.data.subreddit}"

        CItem.ThumbnailString = If(s.data.thumbnail = "default", vbNullString, s.data.thumbnail)

        'CItem.HasPreview = If(s.data.preview IsNot Nothing, True, False)

        Return CItem
    End Function

    ''' <summary>
    ''' Set up the output streams, files, and CsvHelper.CsvWriter
    ''' </summary>
    Private Shared Sub InitializeSheet()
        TWriter = New IO.StreamWriter("C:\$Development\-Side Projects\IoNA\Output.csv", append:=False)
        CWriter = New CsvHelper.CsvWriter(TWriter)

        ' Map out the CsvItemClass headers and types    
        Dim AMap = CWriter.Configuration.AutoMap(GetType(CsvItemClass))

        ' Write the CsvItemClass Headers                
        CWriter.WriteHeader(GetType(CsvItemClass))


    End Sub







End Class
