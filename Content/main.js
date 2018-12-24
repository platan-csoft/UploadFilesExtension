// Скрытие модального окна входа в систему

function hideLoginDlg() {
	$('#authAlert').hide();
	$('#loginModal').modal('hide');
}

// Инициализация главной страницы. Установление видимости для всего пространства страницы, ранее скрытого в целях отображения диалога входа в систему

function showMainWindow() {
	$('#mainpage').show();
	$('#saveSuccess').hide();
	getUserConfig();
}

// Логаут с последующим показом окна входа в систему

function myLogout() {
	if (localStorage.getItem( 'accessToken' ) !== null) localStorage.removeItem( 'accessToken' );
	showLoginDlg();
}

// Показ уведомления о неправильном вводе пароля с последующим затуханием

function showAuthAlert() {
	$('#authAlert').show();
	$('#authAlert').delay(3000).fadeOut();
}	

// Открытие модального окна логина, при этом основное наполнение главной страницы скрывается

function showLoginDlg() {
	
 	$('#authAlert').hide();
	$('#mainpage').hide();
	
	$('#login')[0].value = '';
	$('#pass')[0].value = '';
	
	$('#inputGuid')[0].value = '';
	$('#inputFolder')[0].value = '';
	$('#inputFileDef')[0].value = '';

	$('#loginModal').modal({backdrop: 'static',  keyboard: false, focus: true, show: true});
}

// Получение настроек от сервера TDMS 

function getUserConfig() {
	var token = localStorage.getItem('accessToken');
	if (token == null) {
		showLoginDlg(); return;
	}
		
	var $form =  $( '#settingsForm' ),
	url = $form.attr( 'action' );

	var confXmlHttp = new XMLHttpRequest();
	
	var mimeType = "application/x-www-form-urlencoded";  
	confXmlHttp.open( 'GET', url, true ); 
	confXmlHttp.setRequestHeader( 'Content-Type', mimeType );
	confXmlHttp.setRequestHeader( 'Authorization', 'Bearer ' + token.toString() );
	confXmlHttp.send( '' );
	
		confXmlHttp.onreadystatechange = function() {
		if (confXmlHttp.readyState != 4) return;
		if (confXmlHttp.status == 401) {
			showLoginDlg(); return;
		}
		if (confXmlHttp.status == 200) {
			var settingsParse = JSON.parse(confXmlHttp.responseText);
			$('#inputGuid')[0].value = settingsParse.Guid;
			$('#inputFolder')[0].value = settingsParse.Folder;
			$('#inputFileDef')[0].value = settingsParse.FileDef;
		} 
	};	
}

// Попытка аутентификации -  отправка пользовательских данных на сервер и, в случае успеха, получение токена
 
$("#authForm").submit(function(event) {
	 
	event.preventDefault();
	var b64Token = Base64.encode($("#authForm").find( 'input[name="login"]' ).val()+':'+$("#authForm").find( 'input[name="pass"]' ).val());
	   
	var xmlHttp = new XMLHttpRequest();
    
	var mimeType = "text/plain";  
	xmlHttp.open('PUT', window.location.origin + "/token", true); 
	xmlHttp.setRequestHeader('Content-Type', mimeType);  
	xmlHttp.setRequestHeader('Authorization', 'Basic ' + b64Token);
	xmlHttp.send("grant_type=client_credentials"); 

	xmlHttp.onreadystatechange = function() {
		if (xmlHttp.readyState != 4) return;
		if (xmlHttp.status != 200) {
			 showAuthAlert();
		} 
		else 
		{
			var toknParse = JSON.parse(xmlHttp.responseText);
			localStorage.setItem('accessToken', toknParse.access_token);
			hideLoginDlg();
			showMainWindow();
		}
};
	
});

// Проверка входа при загрузке страницы

$( document ).ready(function() {
			if (localStorage.getItem('accessToken')===null) {
				showLoginDlg();
			}
			else {
				showMainWindow();
			}
});
