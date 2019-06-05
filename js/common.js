<!--
function mOvr(src,clrOver){
	if (!src.contains(event.fromElement)){
		src.style.cursor = 'default';
		src.bgColor = clrOver;
	}
}
function mOut(src,clrIn){
	if (!src.contains(event.toElement)){
		src.style.cursor = 'default';
		src.bgColor = clrIn;
	}
}

function MM_findObj(n, d) { //v4.0
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && document.getElementById) x=document.getElementById(n); return x;
}
function MM_nbGroup(event, grpName) { //v3.0
  var i,img,nbArr,args=MM_nbGroup.arguments;
  if (event == "init" && args.length > 2) {
    if ((img = MM_findObj(args[2])) != null && !img.MM_init) {
      img.MM_init = true; img.MM_up = args[3]; img.MM_dn = img.src;
      if ((nbArr = document[grpName]) == null) nbArr = document[grpName] = new Array();
      nbArr[nbArr.length] = img;
      for (i=4; i < args.length-1; i+=2) if ((img = MM_findObj(args[i])) != null) {
        if (!img.MM_up) img.MM_up = img.src;
        img.src = img.MM_dn = args[i+1];
        nbArr[nbArr.length] = img;
    } }
  } else if (event == "over") {
    document.MM_nbOver = nbArr = new Array();
    for (i=1; i < args.length-1; i+=3) if ((img = MM_findObj(args[i])) != null) {
      if (!img.MM_up) img.MM_up = img.src;
      img.src = (img.MM_dn && args[i+2]) ? args[i+2] : args[i+1];
      nbArr[nbArr.length] = img;
    }
  } else if (event == "out" ) {
    for (i=0; i < document.MM_nbOver.length; i++) {
      img = document.MM_nbOver[i]; img.src = (img.MM_dn) ? img.MM_dn : img.MM_up; }
  } else if (event == "down") {
    if ((nbArr = document[grpName]) != null)
      for (i=0; i < nbArr.length; i++) { img=nbArr[i]; img.src = img.MM_up; img.MM_dn = 0; }
    document[grpName] = nbArr = new Array();
    for (i=2; i < args.length-1; i+=2) if ((img = MM_findObj(args[i])) != null) {
      if (!img.MM_up) img.MM_up = img.src;
      img.src = img.MM_dn = args[i+1];
      nbArr[nbArr.length] = img;
  } }
}

function linkOmatic(sel, win)
{
	win.location.href = sel.options[sel.selectedIndex].value
}


function imprimir() {
	window.print();  
}

function noDblClick(showDiv, hideDiv){
	if ( typeof hideDiv == 'string') {
			hideDiv = MM_findObj(hideDiv)
	}
	hideDiv.style.display='none';

	if ( typeof showDiv == 'string') {
			showDiv = MM_findObj(showDiv)
	}
	showDiv.style.display='inline';	
}

function OpenWin(pUrl,pName,pParam)
{
	window.open(url,name,param);
}

function OpenWinModal(strUrl,strFeatures)
{
	var ReturnVal = true
	if (strFeatures == '')
	strFeatures = "resizable=no;dialogWidth:510px;dialogHeight:350px;help:no;maximize:no;minimize:yes;scrollbars:yes";
	var r = window.showModalDialog(strUrl,ReturnVal,strFeatures);
	if(r == false)
	{
		return false ;
	}  else{
		return r;
	}
}

	/*
 		Abre popup para selectionar un codigo de  cliente
		objName= nombre del objeto al cual el valor será devuelto
	*/
	function findCliente(objName)
		{
			obj = MM_findObj(objName);
	
			if(obj){
					var code = OpenWinModal('/utiles/buscaClientes/main.asp','')
					if (code != false && code != undefined ){
						obj.value = code;
					}
			}else{
				alert("Objeto "+ objName + " no existe.")
			}
		}
		
	/*
 		Abre popup para selectionar un codigo de  proveedor
		objName= nombre del objeto al cual el valor será devuelto
	*/
	function findProveedor(objName)
		{
			obj = MM_findObj(objName);
	
			if(obj){
					var code = OpenWinModal('utiles/buscaProveedores/main.asp','')
					if (code != false && code != undefined ){
						obj.value = code;
					}
			}else{
				alert("Objeto "+ objName + " no existe.")
			}
		}	
		
	/*
 		Abre popup para selectionar un codigo de  familia
			obj= objeto al cual el valor será devuelto (no implementado)
	*/
	function findSubfamilia(objName)
		{
			obj = MM_findObj(objName);
	
			if(obj){
						var mywin
						var param
						var winl = (screen.width - 430) / 2;
						var wint = (screen.height - 185) / 2; 
						param = "width=430,height=185,Top="+wint+",Left="+winl+",toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1";
						mywin = window.open("/utiles/get_subfamilias.aspx","Subfamilias",param );
						mywin.focus();
			}else{
				alert("Objeto "+ objName + " no existe.")
			}

		}	
	/*
 		Abre popup para selectionar un codigo de  familia
 			obj= objeto al cual el valor será devuelto (no implementado)
	*/
	function findFamilia(objName)
		{
			obj = MM_findObj(objName);
			if(obj){
						var mywin
						var param
						var winl = (screen.width - 430) / 2;
						var wint = (screen.height - 185) / 2; 
						param = "width=430,height=185,Top="+wint+",Left="+winl+",toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1";
						mywin = window.open("/utiles/get_familia.aspx","familias",param );
						mywin.focus();
			}else{
				alert("Objeto "+ objName + " no existe.")
			}			
		}	

		
function MM_trim(str)
{
   return str.replace(/^\s*|\s*$/g,"");
}

//-->