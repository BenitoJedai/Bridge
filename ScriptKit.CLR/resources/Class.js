/* Simple JavaScript Inheritance
 * By John Resig http://ejohn.org/
 * MIT Licensed.
 */
(function () { var e = false, t = /xyz/.test(function () { xyz }) ? /\bbase\b/ : /.*/; this.Class = function () { }; Class.extend = function (n) { function o() { if (!e && this.init) this.init.apply(this, arguments) } var r = this.prototype; e = true; var i = new this; e = false; for (var s in n) { i[s] = typeof n[s] == "function" && typeof r[s] == "function" && t.test(n[s]) ? function (e, t) { return function () { var n = this.base; this.base = r[e]; var i = t.apply(this, arguments); this.base = n; return i } }(s, n[s]) : n[s] } o.prototype = i; o.prototype.constructor = o; o.extend = arguments.callee; return o } })()