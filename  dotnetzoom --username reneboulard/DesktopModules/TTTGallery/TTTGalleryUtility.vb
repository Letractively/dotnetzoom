'=======================================================================================
' TTTGALLERY MODULE FOR DOTNETNUKE (1.0.10)
'=======================================================================================
' Original version by:              DAVID BARRETT http://www.davidbarrett.net
' A modified version for DNN by:    KENNYRICE
' This version for TTTCompany       http://www.tttcompany.com
' Author:                           TAM TRAN MINH   (tamttt - tam@tttcompany.com)
' Slideshow component:              FREEK VAN OORT
' Flash Player component:           TYLER JENSEN
' With ideas & code contributed by: 
' JOE BRINKMAN(Jbrinkman), SAM HUNT(Ossy), CLEM MESSERLI(Webguy96), KIMBERLY LAZARSKI(Katse)
' RICHARD COX(RichardCox), ALAN VANCE(Favance), ROB FOULK(Robfoulk), KHOI NGUYEN(khoittt)
'========================================================================================
Option Strict On

Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.IO


Namespace DotNetZoom

    ' Utility routines for various repeated functions.

    Module GalleryGraphics


        'added by Tam - to avoid images have low-resolution after resize using CreatThumb 
        Public Sub ResizeImage(ByVal Source As String, ByVal Destination As String, ByVal MaxWidth As Integer, ByVal MaxHeight As Integer, ByVal Extension As String, ByVal Quality As Integer)
            Dim lWidth As Integer
            Dim lHeight As Integer
            Dim newWidth As Integer
            Dim newHeight As Integer
            Dim sRatio As Double
            Dim mImage As System.Drawing.Image
            Dim iFormat As Imaging.ImageFormat
			Dim myEncoders() As ImageCodecInfo
			myEncoders = ImageCodecInfo.GetImageEncoders()
			Dim numEncoders As Integer = myEncoders.GetLength(0)
			Dim strNumEncoders As String = numEncoders.ToString()
			Dim i As Integer = 0
			Dim z as Integer = -1

			If Quality < 20 then 
			Quality = 20
			else
			If quality > 80 then
			Quality = 80
			end if
			end if
			
            Select Case LCase(Replace(Extension, ".", ""))
                Case "bmp"
                    iFormat = ImageFormat.Bmp
                Case "jpg"
                    iFormat = ImageFormat.Jpeg
					If numEncoders > 0 Then
					For i = 0 To numEncoders - 1
					if MyEncoders(i).MimeType = "image/jpeg" then
					z = i
					End if
					Next i
					End If
                Case "gif"
                    iFormat = ImageFormat.Gif
                Case "tif"
                    iFormat = ImageFormat.Tiff
                Case "png"
                    iFormat = ImageFormat.Png
            End Select

            Try
                mImage = System.Drawing.Image.FromFile(Source)
                lWidth = mImage.Width
                lHeight = mImage.Height

                If Not (lWidth <= MaxWidth AndAlso lHeight <= MaxHeight) Then
                    sRatio = (lHeight / lWidth)
                    If sRatio > (MaxHeight / MaxWidth) Then ' Bounded by height
					    newWidth = CShort(MaxHeight / sRatio)
                        newHeight = MaxHeight
                    Else 'Bounded by width
                        newWidth = MaxWidth
                        newHeight = CShort(MaxWidth * sRatio)
                    End If
 				  	Dim newImage as new System.Drawing.Bitmap(newWidth,newHeight)
					Dim g As Graphics = Graphics.FromImage(newImage)
 					g.InterpolationMode = InterpolationMode.HighQualityBicubic
   					g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
   					g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
   					Dim rect As New Rectangle(0, 0, newWidth,newHeight)
   					g.DrawImage(mImage, rect, 0, 0, mImage.width, mImage.height, GraphicsUnit.Pixel)

					
					Dim PropItm As PropertyItem
					For Each PropItm In mImage.PropertyItems
   						newImage.SetPropertyItem(PropItm)
					Next
		if z <> - 1 then
		Dim encoderInstance As System.Drawing.Imaging.Encoder = System.Drawing.Imaging.Encoder.Quality
		Dim encoderParametersInstance As EncoderParameters = New EncoderParameters (2)
		Dim encoderParameterInstance As EncoderParameter = New EncoderParameter (encoderInstance, Quality)
		encoderParametersInstance.Param(0) = encoderParameterInstance
		encoderInstance = System.Drawing.Imaging.Encoder.ColorDepth
		encoderParameterInstance = New EncoderParameter (encoderInstance, 24)
		encoderParametersInstance.Param(1) = encoderParameterInstance
		newImage.Save(Destination, MyEncoders(z), encoderParametersInstance)
		else
		newImage.Save(Destination, iFormat)
		end if
                newImage.Dispose()
                Else
		if z <> - 1 then
		Dim encoderInstance As System.Drawing.Imaging.Encoder = System.Drawing.Imaging.Encoder.Quality
		Dim encoderParametersInstance As EncoderParameters = New EncoderParameters (2)
		Dim encoderParameterInstance As EncoderParameter = New EncoderParameter (encoderInstance, 50)
		encoderParametersInstance.Param(0) = encoderParameterInstance
		encoderInstance = System.Drawing.Imaging.Encoder.ColorDepth
		encoderParameterInstance = New EncoderParameter (encoderInstance, 24)
		encoderParametersInstance.Param(1) = encoderParameterInstance
		mImage.Save(Destination, MyEncoders(z), encoderParametersInstance)
		else
		mImage.Save(Destination, iFormat)
		end if
        End If
        mImage.Dispose()

            Catch ex As Exception
                Throw ex
            End Try

        End Sub

    End Module


    Module GalleryUtility


        ' Provides more functionality than the path object static functions
        Public Function BuildPath(ByVal Input() As String, ByVal Delimiter As String, ByVal StripInitial As Boolean, ByVal StripFinal As Boolean) As String
            Dim output As StringBuilder = New StringBuilder()

            output.Append(Join(Input, Delimiter))
            output.Replace(Delimiter & Delimiter, Delimiter)

            If StripInitial Then
                If Left(output.ToString(), Len(Delimiter)) = Delimiter Then
                    output.Remove(0, Len(Delimiter))
                End If
            End If

            If StripFinal Then
                If Right(output.ToString, Len(Delimiter)) = Delimiter Then
                    output.Remove(output.Length - Len(Delimiter), Len(Delimiter))
                End If
            End If

            Return output.ToString()

        End Function

        ' Alternate signature for the BuildPath function
        Public Function BuildPath(ByVal Input() As String, ByVal Delimiter As String) As String

            Return BuildPath(Input, Delimiter, True, False)

        End Function

        ' When BuildCacheOnStart is set to true, this recursively populates the folder objects
        Public Sub PopulateAllFolders(ByVal rootFolder As GalleryFolder)
            Dim folder As Object

            If Not rootFolder.IsPopulated Then
                rootFolder.Populate()
            End If

            For Each folder In rootFolder.List
                If TypeOf folder Is GalleryFolder AndAlso Not CType(folder, GalleryFolder).IsPopulated Then
                    CType(folder, GalleryFolder).Populate()
                    PopulateAllFolders(CType(folder, GalleryFolder))
                End If
            Next

        End Sub

    End Module

End Namespace