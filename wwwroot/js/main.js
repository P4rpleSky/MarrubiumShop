$(function() {
    $('.owl-carousel').owlCarousel({
        loop:true,
        margin:39,
        dots:false,
        nav:true,
        navText: [ '', '' ],
        responsive:{
            0:{
                items:1
            },
            600:{
                items:1
            },
            1000:{
                items:2
            }
        }
    });
});