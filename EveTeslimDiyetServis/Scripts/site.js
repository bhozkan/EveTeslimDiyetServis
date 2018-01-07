$( ".responsiveMenu" ).click(function() {
  $( ".topMenu" ). slideToggle(400);
});


$("#calculatecalorie").click(function () {
    var ckn = 0;
    var yas = $('#yasinizcalorie').val();
    var boy = $('#boyunuzcalorie').val();
    var kilo = $('#kilonuzcalorie').val();
    var gender = $('#cinsiyecalorie').val();
    var sport = $('#sportcalorie').val();
    if (yas === '') {
        $('#yasinizcalorie').css('border', '1px solid #ff0000');
        ckn = 1;
    }
    if (boy === '') {
        $('#boyunuzcalorie').css('border', '1px solid #ff0000');
        ckn = 1;
    }
    if (kilo === '') {
        $('#kilonuzcalorie').css('border', '1px solid #ff0000');
        ckn = 1;
    }
    if (gender === '') {
        $('#cinsiyecalorie').css('border', '1px solid #ff0000');
        ckn = 1;
    }
    if (sport === '') {
        $('#sportcalorie').css('border', '1px solid #ff0000');
        ckn = 1;
    }
    if (ckn === 1) {
        return false;
    }
    var carpan = 1.2;
    if (sport === 1) {
        carpan = 1.3;
    }
    var kcal = CalculateCal(parseInt(kilo), parseInt(boy), parseInt(yas), parseInt(gender), carpan);
    console.log(kcal + "   testststst");

    $('#calvalaues').html('<div>Form korumak için almanız gereken günlük kalori:<br/> <b>'
        + Apply_GoalKcal(kcal, false) + ' kkal</b></div>').fadeIn();
    $('#calvalaues1').html('<div>' +
        'Kilo vermek için almanız gereken günlük kalori:<br/><b>' + Apply_GoalKcal(kcal, true) + " kkal</b>" +
        '</div>').fadeIn();
    return false;
});

$('#calculateideal').click(function () {
    var ckn = 0;
    var yas = $('#yasideal').val();
    var boy = $('#boyunuzideal').val();
    var kilo = $('#kilonuzideal1').val();
    var gender = $('#cinsiyetideal').val();
    if (yas === '') {
        $('#yasideal').css('border', '1px solid #ff0000');
        ckn = 1;
    }
    if (boy === '') {
        $('#boyunuzideal').css('border', '1px solid #ff0000');
        ckn = 1;
    }
    if (kilo === '') {
        $('#kilonuzideal1').css('border', '1px solid #ff0000');
        ckn = 1;
    }
    if (gender === '') {
        $('#cinsiyetideal').css('border', '1px solid #ff0000');
        ckn = 1;
    }
    if (ckn === 1) {
        //$.jGrowl('<img src="/images/logo.png" style="display:block;margin:0 auto;" /><center>Lütfen Gerekli Alanları Giriniz.</center>');
        return false;
    }
    var ideal = Calculate_Ideal_Weight(parseInt(boy), parseInt(yas), parseInt(gender));
    var message = '';


    $('#idealvalue').html('<div>İdeal Kilonuz:<br><b>' + ideal + 'kg</b><br>' + message + '</div>').fadeIn();
    //$('#idealtext').text('Vücut kitle endeksiniz 20-25 arasındaysa ideal kilo sınırlarınız içerisindesinizdir. Ancak vücuttaki yağ/kas dağılımınızın dengesizliği sebebiyle kilonuzu fazla görüyor olabilirsiniz. Ayrıntılı bilgi için bize ulaşabilirsiniz.');
    return false;
});

$('#calculatebmi').click(function () {
    var boy = $('#boy').val();
    var kilo = $('#kilo').val();
    if (boy === '' || kilo === '') {

        $.jGrowl('<img src="/images/logo.png" style="display:block;margin:0 auto;" /><center>Lütfen Boy-Kilo Değerlerinizi Giriniz.</center>');
    } else {
        var bmi = Calculate_Bmi(kilo, boy);
        var durum = 'Şişman / Obez';
        var metin = "ETDS yemekleri ile kolay ve sağlıklı kilo verebilirsiniz.";
        if (bmi < 20) {
            durum = 'Zayıf';
            metin = "ETDS size kilo almanız için de destek olur.";
        }
        else if (bmi < 25) {
            durum = 'İdeal Kilo';
            metin = "Formunuzu korumak için ETDS'den faydalanabilirsiniz.";
        }
        else if (bmi < 30) {
            durum = 'Hafif Şişman';
            metin = "Vermeniz gereken bir kaç kiloyu ETDS yemekleri ile kolaylıkla verebilirsiniz.";
        }
        $('#bmivalue').html("<div style='margin-top:10px' class='visible-xs'></div><div>Vücut Kitle End.:<br/> <b>" + bmi + "</b><br />Durum:<br /><b>" + durum + "</b></div>").fadeIn();
        $('#bmitext').text(metin);
    }
    return false;
});

$('#calculaterisk').click(function () {
    var ckn = 0;
    var bel = $('#belcevresi2').val();
    var kalca = $('#kalcasevresi').val();
    var gender = $('#gender4').val();
    if (bel === '') {
        $('#belcevresi2').css('border', '1px solid #ff0000');
        ckn = 1;
    }
    if (kalca === '') {
        $('#kalcasevresi').css('border', '1px solid #ff0000');
        ckn = 1;
    }
    if (gender === '') {
        $('#gender4').css('border', '1px solid #ff0000');
        ckn = 1;
    }
    if (ckn === 1) {
        //$.jGrowl('<img src="/images/logo.png" style="display:block;margin:0 auto;" /><center>Lütfen Gerekli Alanları Giriniz.</center>');
        return false;

    }
    var result = Calculate_Risk(bel, kalca, gender);
    console.log(result);
    var metin ='Bel/kalça oranınız normal';
    var obsktext = 'Formunuzu korumak için ETDS’yı deneyin!';
    if(result==='Riskli'){
        metin='Bel/kalça oranınız normalin üzerinde';
        obsktext='ETDS yemekleri ile kolay ve sağlıklı kilo verebilirsiniz.';
    }
    $('#idealobsk').html('<div style="margin-top:20px">' + metin + '</div>').fadeIn();
    $('#obsktext').text(obsktext);
    return false;
});
$('#calculateobsbelcev').click(function () {
    var ckn = 0;
    var bel = $('#belcevresi1belcev').val();
    var gender = $('#genderbelcev').val();
    if (bel === '') {
        $('#belcevresi1belcev').css('border', '1px solid #ff0000');
        ckn = 1;
    }
    if (gender === '') {
        $('#genderbelcev').css('border', '1px solid #ff0000');
        ckn = 1;
    }
    if (ckn === 1) {
        //$.jGrowl('<img src="/images/logo.png" style="display:block;margin:0 auto;" /><center>Lütfen Gerekli Alanları Giriniz.</center>');
        return false;

    }
    var ideal = Calculate_Obesity(bel, gender);
    var metin = 'Riskiniz bulunmamaktadır';
    var obstext = 'Formunuzu korumak için ETDS’yı deneyin!';
    if (ideal === 'Yüksek Risk Grubu') {
        metin = 'Yüksek risk grubundasınız';
        obstext = 'ETDS yemekleri ile kolay ve sağlıklı kilo verebilirsiniz.';
    }
    else if (ideal === 'Hafif Risk Grubu') {
        metin = 'Hafif risk grubundasınız';
        obstext = 'Vermeniz gereken bir kaç kiloyu ETDS yemekleri ile kolaylıkla verebilirsiniz.';
    }

    $('#idealobs').html('<div style="margin-top:20px">' + metin + '</div>').fadeIn();
    $('#obstext').text(obstext);
    return false;
});
