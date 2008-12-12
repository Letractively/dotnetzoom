var popup = null;
function CreateWnd (file, width, height, resize)
{
	var doCenter = false;

	if((popup == null) || popup.closed)
	{
		attribs = "";
		if(resize) size = "yes"; else size = "no";
		for(var item in window)
			{ if(item == "screen") { doCenter = true; break; } }

		if(doCenter)
		{	
			if(screen.width <= width || screen.height <= height) size = "yes";
			WndTop  = (screen.height - height) / 2;
			WndLeft = (screen.width  - width)  / 2;
			attribs = "width=" + width + ",height=" + height + ",resizable=" + size + ",scrollbars=" + size + "," + 
			"status=no,toolbar=no,directories=no,menubar=no,location=no,top=" + WndTop + ",left=" + WndLeft;
		}
		else
		{
			if(navigator.appName=="Netscape" && navigator.javaEnabled())
			{	
				var toolkit = java.awt.Toolkit.getDefaultToolkit();
				var screen_size = toolkit.getScreenSize();
				if(screen_size.width <= width || screen_size.height <= height) size = "yes";
				WndTop  = (screen_size.height - height) / 2;
				WndLeft = (screen_size.width  - width)  / 2;
				attribs = "width=" + width + ",height=" + height + ",resizable=" + size + ",scrollbars=" + size + "," + 
				"status=no,toolbar=no,directories=no,menubar=no,location=no,top=" + WndTop + ",left=" + WndLeft;
			}
			else
			{	size = "yes";
				attribs = "width=" + width + ",height=" + height + ",resizable=" + size + ",scrollbars=" + size + "," + 
				"status=no,toolbar=no,directories=no,menubar=no,location=no";
			}
		}

		popup = open(file, "", attribs);
	}
	else
	{
		DestroyWnd();
		CreateWnd(file, width, height, resize);
	}
}


function DestroyWnd ()
{
	if(popup != null)
	{
		popup.close();
		popup = null;
	}
}

