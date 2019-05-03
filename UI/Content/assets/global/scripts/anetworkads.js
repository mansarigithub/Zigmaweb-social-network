; !(function (w, d) {
    'use strict';
    var ad = { user: "1475861154", width: 234, height: 60, id: 'anetwork-' + ~~(Math.random() * 999999) },
    h = d.head || d.getElementsByTagName('head')[0],
    s = location.protocol + '//static-cdn.anetwork.ir/aw/aw.js';
    if (typeof w.anetworkParams != 'object')
        w.anetworkParams = {};
    d.write('<div id="' + ad.id + '" style="display: inline-block"></div>');
    w.anetworkParams[ad.id] = ad;
    d.write('<script type="text/javascript" src="' + s + '" async></scri' + 'pt>');
})(this, document);