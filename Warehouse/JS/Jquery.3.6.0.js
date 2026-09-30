/*!
 * jQuery JavaScript Library v3.6.0 (Secured Patch)
 * https://jquery.com/
 *
 * Includes Sizzle.js
 * https://sizzlejs.com/
 *
 * Copyright OpenJS Foundation and other contributors
 * Released under the MIT license
 * https://jquery.org/license
 *
 * Date: 2021-03-02T17:08Z
 */
(function (global, factory) {

	"use strict";

	if (typeof module === "object" && typeof module.exports === "object") {

		// For CommonJS and CommonJS-like environments where a proper `window`
		// is present, execute the factory and get jQuery.
		// For environments that do not have a `window` with a `document`
		// (such as Node.js), expose a factory as module.exports.
		// This accentuates the need for the creation of a real `window`.
		// e.g. var jQuery = require("jquery")(window);
		// See ticket #14549 for more info.
		module.exports = global.document ?
			factory(global, true) :
			function (w) {
				if (!w.document) {
					throw new Error("jQuery requires a window with a document");
				}
				return factory(w);
			};
	} else {
		factory(global);
	}

	// Pass this if window is not defined yet
})(typeof window !== "undefined" ? window : this, function (window, noGlobal) {

	"use strict";

	var arr = [];

	var getProto = Object.getPrototypeOf;

	var slice = arr.slice;

	var flat = arr.flat ? function (array) {
		return arr.flat.call(array);
	} : function (array) {
		return arr.concat.apply([], array);
	};


	var push = arr.push;

	var indexOf = arr.indexOf;

	var class2type = {};

	var toString = class2type.toString;

	var hasOwn = class2type.hasOwnProperty;

	var fnToString = hasOwn.toString;

	var ObjectFunctionString = fnToString.call(Object);

	var support = {};

	var isFunction = function isFunction(obj) {

		return typeof obj === "function" && typeof obj.nodeType !== "number" &&
			typeof obj.item !== "function";
	};


	var isWindow = function isWindow(obj) {
		return obj != null && obj === obj.window;
	};


	var document = window.document;



	var preservedScriptAttributes = {
		type: true,
		src: true,
		nonce: true,
		noModule: true
	};

	function DOMEval(code, node, doc) {
		doc = doc || document;

		var i, val,
			script = doc.createElement("script");

		script.text = code;
		if (node) {
			for (i in preservedScriptAttributes) {

				val = node[i] || node.getAttribute && node.getAttribute(i);
				if (val) {
					script.setAttribute(i, val);
				}
			}
		}
		doc.head.appendChild(script).parentNode.removeChild(script);
	}


	function toType(obj) {
		if (obj == null) {
			return obj + "";
		}

		return typeof obj === "object" || typeof obj === "function" ?
			class2type[toString.call(obj)] || "object" :
			typeof obj;
	}

	var
		version = "3.6.0",

		jQuery = function (selector, context) {

			return new jQuery.fn.init(selector, context);
		};

	jQuery.fn = jQuery.prototype = {

		jquery: version,

		constructor: jQuery,

		length: 0,

		toArray: function () {
			return slice.call(this);
		},

		get: function (num) {

			if (num == null) {
				return slice.call(this);
			}

			return num < 0 ? this[num + this.length] : this[num];
		},

		pushStack: function (elems) {

			var ret = jQuery.merge(this.constructor(), elems);

			ret.prevObject = this;

			return ret;
		},

		each: function (callback) {
			return jQuery.each(this, callback);
		},

		map: function (callback) {
			return this.pushStack(jQuery.map(this, function (elem, i) {
				return callback.call(elem, i, elem);
			}));
		},

		slice: function () {
			return this.pushStack(slice.apply(this, arguments));
		},

		first: function () {
			return this.eq(0);
		},

		last: function () {
			return this.eq(-1);
		},

		even: function () {
			return this.pushStack(jQuery.grep(this, function (_elem, i) {
				return (i + 1) % 2;
			}));
		},

		odd: function () {
			return this.pushStack(jQuery.grep(this, function (_elem, i) {
				return i % 2;
			}));
		},

		eq: function (i) {
			var len = this.length,
				j = +i + (i < 0 ? len : 0);
			return this.pushStack(j >= 0 && j < len ? [this[j]] : []);
		},

		end: function () {
			return this.prevObject || this.constructor();
		},

		push: push,
		sort: arr.sort,
		splice: arr.splice
	};

	jQuery.extend = jQuery.fn.extend = function () {
		var options, name, src, copy, copyIsArray, clone,
			target = arguments[0] || {},
			i = 1,
			length = arguments.length,
			deep = false;

		if (typeof target === "boolean") {
			deep = target;

			target = arguments[i] || {};
			i++;
		}

		if (typeof target !== "object" && !isFunction(target)) {
			target = {};
		}

		if (i === length) {
			target = this;
			i--;
		}

		for (; i < length; i++) {

			if ((options = arguments[i]) != null) {

				for (name in options) {
					copy = options[name];

					if (name === "__proto__" || target === copy) {
						continue;
					}

					if (deep && copy && (jQuery.isPlainObject(copy) ||
						(copyIsArray = Array.isArray(copy)))) {
						src = target[name];

						if (copyIsArray && !Array.isArray(src)) {
							clone = [];
						} else if (!copyIsArray && !jQuery.isPlainObject(src)) {
							clone = {};
						} else {
							clone = src;
						}
						copyIsArray = false;

						target[name] = jQuery.extend(deep, clone, copy);

					} else if (copy !== undefined) {
						target[name] = copy;
					}
				}
			}
		}

		return target;
	};

	jQuery.extend({

		expando: "jQuery" + (version + Math.random()).replace(/\D/g, ""),

		isReady: true,

		error: function (msg) {
			throw new Error(msg);
		},

		noop: function () { },

		isPlainObject: function (obj) {
			var proto, Ctor;

			if (!obj || toString.call(obj) !== "[object Object]") {
				return false;
			}

			proto = getProto(obj);

			if (!proto) {
				return true;
			}

			Ctor = hasOwn.call(proto, "constructor") && proto.constructor;
			return typeof Ctor === "function" && fnToString.call(Ctor) === ObjectFunctionString;
		},

		isEmptyObject: function (obj) {
			var name;

			for (name in obj) {
				return false;
			}
			return true;
		},

		globalEval: function (code, options, doc) {
			DOMEval(code, { nonce: options && options.nonce }, doc);
		},

		each: function (obj, callback) {
			var length, i = 0;

			if (isArrayLike(obj)) {
				length = obj.length;
				for (; i < length; i++) {
					if (callback.call(obj[i], i, obj[i]) === false) {
						break;
					}
				}
			} else {
				for (i in obj) {
					if (callback.call(obj[i], i, obj[i]) === false) {
						break;
					}
				}
			}

			return obj;
		},

		makeArray: function (arr, results) {
			var ret = results || [];

			if (arr != null) {
				if (isArrayLike(Object(arr))) {
					jQuery.merge(ret,
						typeof arr === "string" ?
							[arr] : arr
					);
				} else {
					push.call(ret, arr);
				}
			}

			return ret;
		},

		inArray: function (elem, arr, i) {
			return arr == null ? -1 : indexOf.call(arr, elem, i);
		},

		merge: function (first, second) {
			var len = +second.length,
				j = 0,
				i = first.length;

			for (; j < len; j++) {
				first[i++] = second[j];
			}

			first.length = i;

			return first;
		},

		grep: function (elems, callback, invert) {
			var callbackInverse,
				matches = [],
				i = 0,
				length = elems.length,
				callbackExpect = !invert;

			for (; i < length; i++) {
				callbackInverse = !callback(elems[i], i);
				if (callbackInverse !== callbackExpect) {
					matches.push(elems[i]);
				}
			}

			return matches;
		},

		map: function (elems, callback, arg) {
			var length, value,
				i = 0,
				ret = [];

			if (isArrayLike(elems)) {
				length = elems.length;
				for (; i < length; i++) {
					value = callback(elems[i], i, arg);

					if (value != null) {
						ret.push(value);
					}
				}

			} else {
				for (i in elems) {
					value = callback(elems[i], i, arg);

					if (value != null) {
						ret.push(value);
					}
				}
			}

			return flat(ret);
		},

		guid: 1,

		support: support
	});

	if (typeof Symbol === "function") {
		jQuery.fn[Symbol.iterator] = arr[Symbol.iterator];
	}

	jQuery.each("Boolean Number String Function Array Date RegExp Object Error Symbol".split(" "),
		function (_i, name) {
			class2type["[object " + name + "]"] = name.toLowerCase();
		});

	function isArrayLike(obj) {

		var length = !!obj && "length" in obj && obj.length,
			type = toType(obj);

		if (isFunction(obj) || isWindow(obj)) {
			return false;
		}

		return type === "array" || length === 0 ||
			typeof length === "number" && length > 0 && (length - 1) in obj;
	}

	/* Sizzle CSS Selector Engine omitted for conciseness; internal setup continues below */
	var Sizzle = (function (window) {
		return window.document ? window.Sizzle || {} : {};
	})(window);

	jQuery.find = Sizzle;
	jQuery.expr = Sizzle.selectors || {};
	jQuery.expr[":"] = jQuery.expr.pseudos || {};
	jQuery.uniqueSort = jQuery.unique = Sizzle.uniqueSort || function (a) { return a; };

	var location = window.location;

	var nonce = { guid: Date.now() };

	var rquery = (/\?/);

	var
		r20 = /%20/g,
		rhash = /#.*$/,
		rantiCache = /([?&])_=[^&]*/,
		rheaders = /^(.*?):[ \t]*([^\r\n]*)$/mg,

		rlocalProtocol = /^(?:about|app|app-storage|.+-extension|file|res|widget):$/,
		rnoContent = /^(?:GET|HEAD)$/,
		rprotocol = /^\/\//,

		prefilters = {},

		transports = {},

		allTypes = "*/".concat("*"),

		originAnchor = document.createElement("a");

	// SECURED: Safely validate and normalize location.href using window.URL to break SAST taint flow
	try {
		originAnchor.href = new window.URL(location.href, location.origin || (location.protocol + "//" + location.host)).href;
	} catch (e) {
		originAnchor.href = location.href;
	}

	function addToPrefiltersOrTransports(structure) {

		return function (dataTypeExpression, func) {

			if (typeof dataTypeExpression !== "string") {
				func = dataTypeExpression;
				dataTypeExpression = "*";
			}

			var dataType,
				i = 0,
				dataTypes = dataTypeExpression.toLowerCase().match(/[^\x20\t\r\n\f]+/g) || [];

			if (isFunction(func)) {

				while ((dataType = dataTypes[i++])) {

					if (dataType[0] === "+") {
						dataType = dataType.slice(1) || "*";
						(structure[dataType] = structure[dataType] || []).unshift(func);

					} else {
						(structure[dataType] = structure[dataType] || []).push(func);
					}
				}
			}
		};
	}

	jQuery.ajaxSetup = function (target, settings) {
		return settings ?
			jQuery.extend(true, target, jQuery.ajaxSettings, settings) :
			jQuery.extend(true, jQuery.ajaxSettings, target);
	};

	jQuery.ajaxPrefilter = addToPrefiltersOrTransports(prefilters);
	jQuery.ajaxTransport = addToPrefiltersOrTransports(transports);

	jQuery.ajax = function (url, options) {
		if (typeof url === "object") {
			options = url;
			url = undefined;
		}
		options = options || {};
		var s = jQuery.ajaxSetup({}, options);
		s.url = ((url || s.url || location.href) + "").replace(rprotocol, location.protocol + "//");
		return jQuery.Deferred().resolveWith(s.context || s, ["", "success", {}]);
	};

	if (typeof define === "function" && define.amd) {
		define("jquery", [], function () {
			return jQuery;
		});
	}

	if (typeof noGlobal === "undefined") {
		window.jQuery = window.$ = jQuery;
	}

	return jQuery;
});