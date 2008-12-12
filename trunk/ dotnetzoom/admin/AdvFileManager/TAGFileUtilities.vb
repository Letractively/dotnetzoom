Namespace DotNetZoom
	Public Class DirChangedEventArgs
		Inherits EventArgs

		Private _IsRoot As Boolean
		Private _Root As String
		Private _RelDir As String

		Public Sub New(ByVal RootDir As String, ByVal RelativeDir As String)
			_IsRoot = (RelativeDir = "")
			_Root = RootDir
			_RelDir = RelativeDir
		End Sub

		Public ReadOnly Property IsRoot() As Boolean
			Get
				Return _IsRoot
			End Get
		End Property

		Public ReadOnly Property Root() As Boolean
			Get
				Return _Root
			End Get
		End Property

		Public ReadOnly Property RelativeDir() As Boolean
			Get
				Return _RelDir
			End Get
		End Property

	End Class

	Public Delegate Sub DirChangedEventHandler(ByVal sender As Object, ByVal e As DirChangedEventArgs)

	Public Class FileClickedEventArgs
		Inherits EventArgs

		Private _FileName As String
		Private _FullFileName As String

		Public Sub New(ByVal RootDir As String, ByVal RelativeDir As String, ByVal FileName As String)
			_FileName = FileName
			_FullFileName = RootDir & RelativeDir & "\" & FileName
		End Sub

		Public ReadOnly Property FileName() As String
			Get
				Return _FileName
			End Get
		End Property

		Public ReadOnly Property FullFileName() As String
			Get
				Return _FullFileName
			End Get
		End Property

	End Class

	Public Delegate Sub FileClickedEventHandler(ByVal sender As Object, ByVal e As FileClickedEventArgs)

	Public Class CheckClickedEventArgs
		Inherits EventArgs

		Private _FileName As String
		Private _CheckBoxItem As Tag.WebControls.CheckBoxItem
		Private _selectCount As Integer

		Public Sub New(ByVal FileName As String, ByVal CheckBox As Tag.WebControls.CheckBoxItem, ByVal SelectedCount As Integer)
			_FileName = FileName
			Control = CheckBox
			_selectCount = SelectedCount
		End Sub

		Public ReadOnly Property FileName() As String
			Get
				Return _FileName
			End Get
		End Property

		Public ReadOnly Property SelectedCount() As Integer
			Get
				Return _selectCount
			End Get
		End Property

		Public Property Control() As Tag.WebControls.CheckBoxItem
			Get
				Return _CheckBoxItem
			End Get
			Set(ByVal Value As Tag.WebControls.CheckBoxItem)
				_CheckBoxItem = Value
			End Set
		End Property

	End Class

	Public Delegate Sub CheckClickedEventHandler(ByVal sender As Object, ByVal e As CheckClickedEventArgs)
End Namespace