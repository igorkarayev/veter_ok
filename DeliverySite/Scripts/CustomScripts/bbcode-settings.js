// ----------------------------------------------------------------------------
// markItUp!
// ----------------------------------------------------------------------------
// Copyright (C) 2011 Jay Salvat
// http://markitup.jaysalvat.com/
// ----------------------------------------------------------------------------
// Html tags
// http://en.wikipedia.org/wiki/html
// ----------------------------------------------------------------------------
// Basic set. Feel free to add more tags
// ----------------------------------------------------------------------------
var myBBCodeSettings = {
	markupSet:  [ 	
		{ name: 'Bold', key: 'B', openWith: '[b]', closeWith: '[/b]' },
        { name: 'Italic', key: 'I', openWith: '[i]', closeWith: '[/i]' },
        { name: 'Underline', key: 'U', openWith: '[u]', closeWith: '[/u]' },
        { name: 'Bulleted list', openWith: '[list]', closeWith: '[/list]' },
        { name: 'List item', openWith: '[*] ' },
        { name: 'Link', key: 'L', openWith: '[url=[![Url]!]]', closeWith: '[/url]', placeHolder: 'название ссылки' },
	]
}
