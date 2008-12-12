'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2008
' by René Boulard ( http://www.reneboulard.qc.ca)'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'

Imports System

Namespace DotNetZoom

    Public Interface IModuleCommunicator

        Event ModuleCommunication As ModuleCommunicationEventHandler
    End Interface 'IModuleCommunicator
    _

    Public Interface IModuleListener
        Sub OnModuleCommunication(ByVal s As Object, ByVal e As ModuleCommunicationEventArgs)
    End Interface 'IModuleListener


    Public Delegate Sub ModuleCommunicationEventHandler(ByVal sender As Object, ByVal e As ModuleCommunicationEventArgs)
    _

    Public Class RoleChangeEventArgs
        Inherits ModuleCommunicationEventArgs
        Private _RoleId As String = Nothing
        Private _PortalId As String = Nothing
        Public Property PortalId() As String
            Get
                Return _PortalId
            End Get
            Set(ByVal Value As String)
                _PortalId = Value
            End Set
        End Property


        Public Property RoleId() As String
            Get
                Return _RoleId
            End Get
            Set(ByVal Value As String)
                _RoleId = Value
            End Set
        End Property

    End Class 'ModuleCommunicationEventArgs

    Public Class ModuleCommunicationEventArgs
        Inherits System.EventArgs

        Private _Text As String = Nothing

        Public Property [Text]() As String
            Get
                Return _Text
            End Get
            Set(ByVal Value As String)
                _Text = Value
            End Set
        End Property

        Public Sub New()
        End Sub 'New


        Public Sub New(ByVal [text] As String)
            _Text = [text]
        End Sub 'New
    End Class 'ModuleCommunicationEventArgs
    _ 


    Public Class ModuleCommunicators
        Inherits System.Collections.CollectionBase


        Default Public Property Item(ByVal index As Integer) As IModuleCommunicator
            Get
                Return CType(Me.List(index), IModuleCommunicator)
            End Get
            Set(ByVal Value As IModuleCommunicator)
                Me.List(index) = Value
            End Set
        End Property

        Public Sub New()
        End Sub 'New


        Public Function Add(ByVal item As IModuleCommunicator) As Integer
            Return Me.List.Add(item)
        End Function 'Add
    End Class 'ModuleCommunicators
    _ 

    Public Class ModuleListeners
        Inherits System.Collections.CollectionBase


        Default Public Property Item(ByVal index As Integer) As IModuleListener
            Get
                Return CType(Me.List(index), IModuleListener)
            End Get
            Set(ByVal Value As IModuleListener)
                Me.List(index) = Value
            End Set
        End Property

        Public Sub New()
        End Sub 'New


        Public Function Add(ByVal item As IModuleListener) As Integer
            Return Me.List.Add(item)
        End Function 'Add

    End Class 'ModuleListeners

End Namespace