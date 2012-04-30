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
' For DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2009
' by René Boulard ( http://www.reneboulard.qc.ca)'
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
        Public Sub ResizeImage(ByVal Source As String, ByVal Destination As String, ByVal MaxWidth As Integer, ByVal MaxHeight As Integer, ByVal Extension As String, ByVal Quality As Integer, Optional ByVal strwatermark As String = "")
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
            Dim z As Integer = -1

            If Quality < 20 Then
                Quality = 20
            Else
                If Quality > 80 Then
                    Quality = 80
                End If
            End If

            Select Case LCase(Replace(Extension, ".", ""))
                Case "bmp"
                    iFormat = ImageFormat.Bmp
                Case "jpg"
                    iFormat = ImageFormat.Jpeg
                    If numEncoders > 0 Then
                        For i = 0 To numEncoders - 1
                            If myEncoders(i).MimeType = "image/jpeg" Then
                                z = i
                            End If
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
                    Dim newImage As New System.Drawing.Bitmap(newWidth, newHeight)
                    Dim g As Graphics = Graphics.FromImage(newImage)
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic
                    g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
                    g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    Dim rect As New Rectangle(0, 0, newWidth, newHeight)
                    g.DrawImage(mImage, rect, 0, 0, mImage.Width, mImage.Height, GraphicsUnit.Pixel)

                    If strwatermark <> "" Then
                        newImage = watermrk(newImage, strwatermark)
                    End If

                    Dim PropItm As PropertyItem
                    For Each PropItm In mImage.PropertyItems
                        newImage.SetPropertyItem(PropItm)
                    Next
                    If z <> -1 Then
                        Dim encoderInstance As System.Drawing.Imaging.Encoder = System.Drawing.Imaging.Encoder.Quality
                        Dim encoderParametersInstance As EncoderParameters = New EncoderParameters(2)
                        Dim encoderParameterInstance As EncoderParameter = New EncoderParameter(encoderInstance, Quality)
                        encoderParametersInstance.Param(0) = encoderParameterInstance
                        encoderInstance = System.Drawing.Imaging.Encoder.ColorDepth
                        encoderParameterInstance = New EncoderParameter(encoderInstance, 24)
                        encoderParametersInstance.Param(1) = encoderParameterInstance
                        newImage.Save(Destination, myEncoders(z), encoderParametersInstance)
                    Else
                        newImage.Save(Destination, iFormat)
                    End If
                    newImage.Dispose()
                Else

                    Dim newImage As System.Drawing.Bitmap
                    newImage = DirectCast(System.Drawing.Bitmap.FromFile(Source), System.Drawing.Bitmap)
                    If strwatermark <> "" Then
                        newImage = watermrk(newImage, strwatermark)
                    End If

                    Dim PropItm As PropertyItem
                    For Each PropItm In mImage.PropertyItems
                        newImage.SetPropertyItem(PropItm)
                    Next

                    If z <> -1 Then
                        Dim encoderInstance As System.Drawing.Imaging.Encoder = System.Drawing.Imaging.Encoder.Quality
                        Dim encoderParametersInstance As EncoderParameters = New EncoderParameters(2)
                        Dim encoderParameterInstance As EncoderParameter = New EncoderParameter(encoderInstance, 50)
                        encoderParametersInstance.Param(0) = encoderParameterInstance
                        encoderInstance = System.Drawing.Imaging.Encoder.ColorDepth
                        encoderParameterInstance = New EncoderParameter(encoderInstance, 24)
                        encoderParametersInstance.Param(1) = encoderParameterInstance
                        newImage.Save(Destination, myEncoders(z), encoderParametersInstance)
                    Else
                        newImage.Save(Destination, iFormat)
                    End If
                    newImage.Dispose()
                End If
                mImage.Dispose()

            Catch ex As Exception
                Throw ex
            End Try

        End Sub


        Public Function watermrk(ByVal bmp As System.Drawing.Bitmap, ByVal strwatermark As String) As System.Drawing.Bitmap
            Dim canvas As Graphics = Graphics.FromImage(bmp)
            Dim StringSizeF As SizeF, DesiredWidth As Single, wmFont As Font, RequiredFontSize As Single, Ratio As Single
            wmFont = New Font("Verdana", 12, FontStyle.Bold)
            RequiredFontSize = 12
            StringSizeF = canvas.MeasureString(strwatermark, wmFont)
            DesiredWidth = CType(bmp.Width * 0.95, Single)
            If StringSizeF.Width > DesiredWidth Then
                Ratio = StringSizeF.Width / wmFont.SizeInPoints
                RequiredFontSize = DesiredWidth / Ratio
                wmFont = New Font("Verdana", RequiredFontSize, FontStyle.Bold)
                StringSizeF = canvas.MeasureString(strwatermark, wmFont)
            End If

            Dim X, Y, Z As Single
            wmFont = New Font("Verdana", RequiredFontSize, FontStyle.Bold)
            Y = CType(bmp.Height - (wmFont.Height + 3), Single)
            X = CType(bmp.Width - (StringSizeF.Width + 3), Single)
            If bmp.Width > 400 Then
                Z = 2
            Else
                Z = 1
            End If

            canvas.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias

            'canvas.DrawString(strwatermark, wmFont, New SolidBrush(System.Drawing.ColorTranslator.FromHtml("#cc0000")), X, Y)
            canvas.DrawString(strwatermark, wmFont, New SolidBrush(Color.Beige), X, Y)
            canvas.DrawString(strwatermark, wmFont, New SolidBrush(Color.FromArgb(120, 0, 0, 0)), X + Z, Y + Z)
            canvas.DrawString(strwatermark, wmFont, New SolidBrush(Color.FromArgb(136, 255, 255, 255)), X, Y)
            canvas.DrawString(strwatermark, wmFont, New SolidBrush(Color.FromArgb(120, 0, 0, 0)), X + Z, Y + Z)
            canvas.DrawString(strwatermark, wmFont, New SolidBrush(Color.FromArgb(136, 255, 255, 255)), X, Y)

            bmp.SetResolution(96, 96)
            canvas.Dispose()
            Return bmp
        End Function


    End Module





    Module GalleryUtility

        Public Function CryptoUrl(ByVal Input As String, ByVal IsPrivate As Boolean) As String
            If IsPrivate Then
                Dim objSecurity As New PortalSecurity()
                Return objSecurity.EncryptURL(System.Web.HttpContext.Current.Application("cryptokey").ToString, Input)
            Else
                Return Input
            End If
        End Function
	
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
            ' Dim folder As Object

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

        Public Sub UpdateFolderSize(ByVal Config As GalleryConfig, Optional ByVal FileSize As Double = 0)
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)
		
            If _portalSettings.HostSpace <> 0 Or Config.Quota <> 0 Then
                Dim StrFolder As String
                Dim objAdmin As New AdminDB()

                If FileSize = 0 Then
                    If _portalSettings.HostSpace <> 0 Then
                        StrFolder = HttpContext.Current.Request.MapPath(_portalSettings.UploadDirectory)
                        objAdmin.AddDirectory(StrFolder, objAdmin.GetFolderSizeRecursive(StrFolder).ToString())
                    End If
                    StrFolder = HttpContext.Current.Request.MapPath(Config.RootURL)
                    objAdmin.AddDirectory(StrFolder, objAdmin.GetFolderSizeRecursive(StrFolder).ToString())
                Else
                    If _portalSettings.HostSpace <> 0 Then
                        StrFolder = HttpContext.Current.Request.MapPath(_portalSettings.UploadDirectory)
	                    objAdmin.AddDirectory(StrFolder, (objAdmin.GetdirectorySpaceUsed(StrFolder) + FileSize).ToString())
                    End If
                    StrFolder = HttpContext.Current.Request.MapPath(Config.RootURL)
                     objAdmin.AddDirectory(StrFolder, (objAdmin.GetdirectorySpaceUsed(StrFolder) + FileSize).ToString())
                    End If
            End If
        End Sub
		
		
		
    End Module

End Namespace