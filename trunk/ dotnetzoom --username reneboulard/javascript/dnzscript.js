function toggleBox(szDivID, iState) // 1 visible, 0 hidden
{
  if(document.layers)
  {document.layers[szDivID].visibility = iState ? "show" : "hide";}
  else if(document.getElementById)
  {var obj = document.getElementById(szDivID);
  obj.style.visibility = iState ? "visible" : "hidden";}
  else if(document.all)
  {document.all[szDivID].style.visibility = iState ? "visible" : "hidden";}
}
  
function protect(name, address, display, subject){
var link = name + "@" + address
if(!display) { display = link; }
if(subject!=null&&subject!="") { link += '?subject=' + subject; }
document.write("<a href='mailto:" + link + "'>" + display + "</a>");
}

function SmartScroller_GetCoords() {
var scrollX, scrollY;
if (document.all) {
if (!document.documentElement.scrollLeft)
scrollX = document.body.scrollLeft;
else
scrollX = document.documentElement.scrollLeft;
if (!document.documentElement.scrollTop)
scrollY
else
scrollY = document.documentElement.scrollTop; }
else {
scrollX = window.pageXOffset; scrollY = window.pageYOffset; }
document.getElementById('scrollLeft').value = scrollX;
document.getElementById('scrollTop').value = scrollY;
}

function SmartScroller_Scroll() {
var x = document.getElementById('scrollLeft').value;
var y = document.getElementById('scrollTop').value;
window.scrollTo(x, y); }

// window.onload = SmartScroller_Scroll;
window.onscroll = SmartScroller_GetCoords;
window.onclick = SmartScroller_GetCoords; 
window.onkeypress = SmartScroller_GetCoords;


function ShowHide(eId, thisImg, state) {
if (e = document.getElementById(eId))
{
    if (state == null)
    {
    state = e.style.display == 'none';
    e.style.display = (state ? '' : 'none');
    }
        if (state == false)
        { 
        document.getElementById(thisImg).src="images/1x1.gif";
        document.getElementById(thisImg).alt="+";
        document.getElementById(thisImg).style.backgroundPosition="0px -407px";
        }
        if (state == true)
        {
        document.getElementById(thisImg).src="images/1x1.gif";
        document.getElementById(thisImg).alt="-";
        document.getElementById(thisImg).style.backgroundPosition="0px -377px";
        }
    }
}


