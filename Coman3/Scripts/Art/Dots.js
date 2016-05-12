$(function () {
    var canvas = document.getElementById('backgroundCanvas'), ctx = canvas.getContext('2d');
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
    

    var dotsSettings = {
        //All per 100 Pixels
        number: 7,
        scale: 0.8,
        speed: 0.45
    };
    var dots;

    function colorValue(min) {
        return Math.floor(Math.random() * 255 + min);
    }

    function createColorStyle(r, g, b) {
        return 'rgba(' + r + ',' + g + ',' + b + ', 0.8)';
    }

    function mixComponents(comp1, weight1, comp2, weight2) {
        return (comp1 * weight1 + comp2 * weight2) / (weight1 + weight2);
    }

    function averageColorStyles(dot1, dot2) {
        var color1 = dot1.color,
            color2 = dot2.color;

        var r = mixComponents(color1.r, dot1.radius, color2.r, dot2.radius),
            g = mixComponents(color1.g, dot1.radius, color2.g, dot2.radius),
            b = mixComponents(color1.b, dot1.radius, color2.b, dot2.radius);
        
        return createColorStyle(Math.floor(r), Math.floor(g), Math.floor(b));
    }

    function color(min) {
        min = min || 0;
        this.r = colorValue(min);
        this.g = colorValue(min);
        this.b = colorValue(min);
        this.style = createColorStyle(this.r, this.g, this.b);
    }

    function dot() {
        this.x = Math.random() * canvas.width;
        this.y = Math.random() * canvas.height;

        var radius = Math.random();
        this.radius = radius;
        
        this.vx = (-Math.random() * -radius * dotsSettings.speed) + (Math.random() * -radius * dotsSettings.speed);
        this.vy = (-Math.random() * -radius * dotsSettings.speed) + (Math.random() * -radius * dotsSettings.speed);
        
        this.color = new color();
    }

    dot.prototype = {
        draw: function () {
            ctx.beginPath();
            ctx.fillStyle = this.color.style;
            ctx.arc(this.x, this.y, this.radius, 0, Math.PI * 2, false);
            ctx.fill();
        }
    };

    function createDots() {
        ctx.lineWidth = 0.35;
        ctx.strokeStyle = (new color(80)).style;
        dots = {
            nb: Math.floor(dotsSettings.number * ((canvas.width * dotsSettings.scale / 100) + canvas.height * dotsSettings.scale / 100)),
            distance: 75,
            array: []
        };
        console.log(dots);
        for (i = 0; i < dots.nb; i++) {
            dots.array.push(new dot());
        }
    }

    function moveDots() {
        for (i = 0; i < dots.nb; i++) {
            var dot = dots.array[i];

            if (dot.y < 0 || dot.y > canvas.height) {
                dot.vx = dot.vx;
                dot.vy = -dot.vy;
            } else if (dot.x < 0 || dot.x > canvas.width) {
                dot.vx = -dot.vx;
                dot.vy = dot.vy;
            }
            dot.x += dot.vx;
            dot.y += dot.vy;
        }
    }

    function drawLines() {
        for (var i = 0; i < dots.nb; i++) {
            for (var j = 0; j < dots.nb; j++) {
                var iDot = dots.array[i];
                var jDot = dots.array[j];

                if ((iDot.x - jDot.x) < dots.distance && (iDot.y - jDot.y) < dots.distance &&
                    (iDot.x - jDot.x) > -dots.distance && (iDot.y - jDot.y) > -dots.distance) {
                    ctx.beginPath();
                    ctx.strokeStyle = averageColorStyles(iDot, jDot);
                    ctx.moveTo(iDot.x, iDot.y);
                    ctx.lineTo(jDot.x, jDot.y);
                    ctx.stroke();
                    ctx.closePath();
                }
            }
        }
    }

    function animateDots() {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        moveDots();
        drawLines();
        requestAnimationFrame(animateDots);
    }
    var rtime;
    var timeout = false;
    var delta = 50;
    $(window).resize(function () {
        rtime = new Date();
        if (timeout === false) {
            timeout = true;
            setTimeout(resetViewport, delta);
        }
    });
    function resetViewport() {
        if (new Date() - rtime < delta) {
            setTimeout(resetViewport, delta);
        } else {
            canvas.width = window.innerWidth;
            canvas.height = window.innerHeight;
            timeout = false;
            dots.array = [];
            createDots();
        }
    }

    createDots();
    requestAnimationFrame(animateDots);
});