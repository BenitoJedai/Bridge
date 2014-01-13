Class.extend('TestProject.App', {
  $extend: [Object],
  $statics : {
    init: function{
      this.i = 5;
    },
    Run : function() {
      var sum = TestProject.App.Sum(5, 6);
    },
    Sum : function(x1, x2) {
      return x1 + x2;
    }
  }
});

Class.extend('TestProject.Class1', {
  $extend: [Object],
  $statics : {
    init: function{
      this.property0 = 0;
    },
    getProperty0 : function () {
      return this.property0;
    },
    setProperty0 : function (value) {
      this.property0 = value;
    }
  },
  init: function() {
    this.b1 = false;
    this.f1 = 0;
    this.property1 = 0;
    this.s1 = null;

    /*  comment here */
    var fn = function(s) {
      return s;
    };
  },
  getProperty1 : function () {
    return this.property1;
  },
  setProperty1 : function (value) {
    this.property1 = value;
  },
  getProperty2 : function () {
    return this.getProperty1()++;
  }
  ,
  setProperty2 : function (value) {
    var iii = 0;
    this.setProperty1(iii);
  }
  ,
  Run : function() {
    var sum = this.Sum(5, 6);
  },
  Sum : function(x1, x2) {
    return x1 + x2;
  }
});

Class.extend('TestProject.Class2', {
  $extend: [TestProject.Class1],
  init: function() {
    this.base();
  },
  Sum : function(x1, x2) {
    var i = TestProject.Class1.getProperty0();
    var c1 = new ();
    var i2 = c1.getProperty1();
    return this.base(x1, x2) + x2 + x1;
  }
});

