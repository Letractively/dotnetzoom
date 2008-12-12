
var TempY = 116;
var TempX = 86;

	function findPosX(obj)
	  {
	    var curleft = 0;
	    if(obj.offsetParent)
	        while(1) 
	        {
	          curleft += obj.offsetLeft;
	          if(!obj.offsetParent)
	            break;
	          obj = obj.offsetParent;
	        }
	    else if(obj.x)
	        curleft += obj.x;
	    return curleft;
  }

	  function findPosY(obj)
	  {
	    var curtop = 0;
	    if(obj.offsetParent)
	        while(1)
	        {
	          curtop += obj.offsetTop;
	          if(!obj.offsetParent)
	            break;
	          obj = obj.offsetParent;
	        }
	    else if(obj.y)
	        curtop += obj.y;
	    return curtop;
	  }

	  var dc = document.getElementById('tigramenu');

	  if(dc != null)
	  	{
	  TempY = findPosY(dc);
	  TempX = findPosX(dc);
	  	}
var MENU_POS1 = [
{'height': 24 ,
'width': 90 ,
'block_top': TempY ,
'block_left': TempX ,
'top': 0 ,
'left': 90 ,
'hide_delay': 1000 ,
'expd_delay': 100 ,
'css' : {
'outer' : ['m0l0oout', 'm0l0oover' , 'menu0downo'],
'inner' : ['m0l0iout', 'm0l0iover' ,'m0l0odown']
}
},
{
'height': 24 ,
'width': 190 ,
'block_top': 25 ,
'block_left':  0 ,
'top': 24 ,
'left': 0
},
{
'height': 24 ,
'width': 190 ,
'block_top': 0 ,
'block_left': 180 ,
'top': 24 ,
'left': 0
}
]

