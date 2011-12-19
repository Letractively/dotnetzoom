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

Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Data.SqlTypes
Imports System.Reflection
Imports DotNetZoom.TTTUtils

Namespace DotNetZoom

    Public Class TTT_GalleryDB


        Public Function TTTGallery_GetIntegratedForum(ByVal GalleryID As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(GetDBConnectionString)

            ' Generate Command Object based on Method
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {GalleryID})

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result
        End Function


    End Class

End Namespace