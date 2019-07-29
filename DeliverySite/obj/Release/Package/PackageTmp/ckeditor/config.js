/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
	config.language = 'ru';
	// config.uiColor = '#AADC6E';
config.toolbar_Full = [
	{ name: 'document', items : [ 'Undo','Redo'] },
	{ name: 'basicstyles', items : [ 'Bold','Italic','Underline','Format' ] },
	{ name: 'paragraph', items : [ 'NumberedList','BulletedList','-','JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock' ] },
	'/',
	{ name: 'colors', items : [ 'TextColor','BGColor' ] },
	{ name: 'links', items : [ 'Link','Unlink' ] },
	{ name: 'insert', items : [ 'Image','Table' ] },
	{ name: 'tools', items : [ 'Source'] }
];
};
