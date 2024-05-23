using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Sneaker_Store.Model;
using Sneaker_Store.Services;
using static Sneaker_Store.Services.ISkoRepository;

public class SkoRepository : ISkoRepository
    {
        private List<Sko> _sko;

        public SkoRepository(bool mockData = false)
        {
            _sko = new List<Sko>();

            if (mockData)
            {
                PopulateSko();
            }
        }

        private void PopulateSko()
        {
            _sko.Add(new Sko(1, "Nike", "Air Max", 44, 1000, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRwbjaVrfAO3HsegBQoUf8m4Is-trQDOmiJlw&usqp=CAU"));
            _sko.Add(new Sko(2, "Asics", "Gel-1130", 38, 850, "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAoHCBQVFBcVFRUXFxcaGxsbGBoYGxogHBodGhohGhgaIBsbJC8mHR0pJhscJTYmKS4wMzMzGyI5PjkyPi4yMzABCwsLEA4QGhISHTIpISkyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMv/AABEIAMIBAwMBIgACEQEDEQH/xAAcAAEAAgMBAQEAAAAAAAAAAAAABQYDBAcCAQj/xABGEAACAQIEAwUDBwsBBwUAAAABAhEAAwQSITEFIkEGEzJRYQdxgSNCUpGhscEUM2JygpKi0dLh8EMVF2NzssLxFiRTg+L/xAAXAQEBAQEAAAAAAAAAAAAAAAAAAQID/8QAHREBAQEBAAMBAQEAAAAAAAAAAAERAhIhMVETA//aAAwDAQACEQMRAD8A7NSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlKDw7gbmKjuJ8UW2hIGYyqgTGrsFWTBgayTB0B0O1RGD4u11rwOYtavXLcKnRCMonXdSrbDc+VYuLq7ICil2S4jgHKDcyEkqNfEPKRMQYmRc9E+o+17Q7ZdkCMzLmzZQ2UBPG2bKTlEEzFWThHHkvMUMowbLDK6nMVzZSrgEPGsa6a+7mGEwAxF+4mFwPeXLbnPcvXvk7TsSXE22Kt5wJIJOmlS1vhd2wyrirOSTCvgrdm4k7CUNo3RE6lVbqdJ0iuq0qHwQu2woa53i/8QBXHrmUAGB0Kj39KlFuqdiKIyUpSgUpSgUpSgUpSgUpSgUpSgUpSgUpSgUpSgUpSgxXLgUSSANN/XQCq5xm2uIBVmvARy9zce2RI8QyMudh5NI02r12p4ibbWkXMSWMqviabdyAJ0nTSSNSvUio3DnvObRgSTmAzBo0zK5hUeQZX0+Fa5gpeF9oGOwbNhrvd3jadkzvOZ1B5TnVo10MwSJ1mK6j2Y7QW8bZF23oZyup3Rh0PmOoPWuR+0rhZS6mIUcrqLd08oIuKDlzKuxKCARoe78983sf40trFXLLtAvKAs7Z1JKj4hmHvgVLB3GlKVApSlByDtZim4dxVrp/MYtVYkzlW4gyFoAJJXRiBuLh10rDxTtTcxD28NhMpuXBzOqiEU7uo8StGskiNNASCJf2vcZwpsjBlO9xBZCgU62idiT5sOXL1DSY0mG7O8IGEtvnjvnHOwgqo6Ivu8+pnyArUovHBsFbweGt2bIyrEsT4mbqzHzPpoAIEARX1Rda8jhlAUMNQfnRroZMBdvWqZw/PbuXCLl13uOpyXHzIpMjKi+RJ+ACjpNWCxxG4qZrhCDcEJduEwDyxbUjNtpM76SKCQ7QYTEPbJt4nunEEEW0ZTHzSrzI9RB9/V2Ru4hrbnEoqujZVZHzLc0nOqwCnlB1320rSftPaK5yx7sBpdrd5F0ICrndO7UmSDmcQYHXTdwDqxa5buzaYNOQB1JkfKLctkhZBGhM6SAIaYLVbvqwBDAgiRB0PuPWs9cpwWFOCxKLhMSlu27rnwt9xDBmXObau3eLchjuDJGpAgHpnfEaRNMG1SsSXQff5VlqBSlKBSlKBSlKBSlKBSlKBSlKBSlKBSlYMTeCIznZVJPwE0HIO1PFjcxl45lfDoSr5c3eW9Lad4FIghHRTInRm6ExYOBcQF0ZXYG6FUsYLq6nUXJMWyjSIdRI2bWZ5dcZ0utcVmVmdwC2T5mbOkMcmUKWDZ2gjpBg5uHYxbdpHS47W0IbxKL9hiQM9ueW4jEkMsEasGAnvDZcXHXuLcOTE2Hs3MxRwJytLoQQyMsQpAIBg6aRXGOPcDv4K6A/me7uLorhdZUg8rjSVmR6iDXS+D9pZVe9dGtzCX0/Nz80MhI7h9uRpUzysdBVjv2bd5DauqlxHA5TqrxrICnlbrMyOla+oqXZn2phUW3jEdmAjvUgk/rIY19Rv5VcsP284awn8pRfRwyn+IVSeK+ze0QWsXWteSXeZAfLMOcA+Zzb9agv93eNU6mzHUobjEeuV1WR6gn41MHX/wD1XgIn8ssR/wAxPumap3ar2lID3GAHe3n0FyBkWdOXN4j6nlHrtVbs+z5iA/fi8innS0jIV2gMJNxSZ05QABJIFWLg3EeH4dBaGFSwzrs+R+8jp3z8tzfWGKjzFZXEZ2c7MXbBfEXWS5iHmTzPkzkzDm5bBdoMkPMaCQxJkzdMnvLCKXzQUu3QlyYMhgj2weihnXy8jTHYAKSwAweVZDWbjIACZlVGa2Qdy3djSTNYbN3Gcty3cw2LQbOVAcACFHe2mIO8czJ1IGlNMS2DsbrC2xGqWpa5BaQHnNnUZti+WCNPKSNtdiF1yjmAeYHVCcqH4Dyqn3OMi2ALtu/YE76XbZaDsy5bhM68pYVLcP42HMWnt3TpIsuoZj1DWnC3I9dTv51qdRMSuJ4ertJzZ+bmLv3oB2CtbiUkTlJjeq3iOzNq1cF1ktXY1bvUTP4SxJGXIigxOfXbnJhWnFxwKn/TAEsGDIq9QTcfmExoMuvQEVXL3aN7j5cIFygsfym6gjklS1m2W3GYqbjGJgFgSFK2GJrEnJci2DaykFB3bDMB4TkIEgwdBrvUlwviFy/bVu8Vbg/OICSBMcp0DB9Oo0nrUVgMLctrlbvLjAlytx2IzsoDPcuMBk3UrbQKTMkANyyC4C7zM17M7HmIQIg0hQYYPkUaAK+xJIJM0wZeG8dtlgr5lYeIsrpB1+ZcCuolSJIgnaatVq6CJn4+dUrBWrhDi/bW42UAG1OoWBOV5J3LE5iBKDcmM1w3ktNbz/Jqh5hmW4hU6lhEFRqSwPT40ouQcede6pfCuJuQLbMFuhdM2qOAAAyOT5wSCSRME6qTYbPEIUm5ylQSw9AJkVMEnStbDYxLk5GBjf0rZqBSlKBSlKBSlKBSlKBSlKBUfxsTYuLMFkZR11YRt18/hUhVS7d8SFhcNmbKHvwTAMDu3AOvQMUPwpByXiuEZC6XBkiJYrnTQcneIAWiNFuqJKmIbZdE4W4rW8rQ7FspIzlyokd04+TaQIgEMDpE11TF8Ot3wCYVhKq3KcrHe2xAJuLLMR82qriuy9y0XyvFt/FFtWBYnlW5hZIYaGGUHfQTArV5NV7CcRHeMUP5Lf8AC4ZYs3Cd1u24PdOT6FJ1yrq1WXhHGTabujNl4lsNcYC28gAPZuknuwYnUujdGG1Rl3CNqlxA2Vdj8oirpIVWcXrI1Eqrn9QbV5t4eEy5Lly0CGNs/Koubd0uKEvYUxOr2xtBJGhyrouF4ushWlH0GWDmHTLcOgn1BI0MxW+L48wCNSCczL6iNCPjXNMFxRwRatF8baKZihg3barsodDkuRJIWNfoTvO8I4hbuSti6Rlkd0Yt3rZLElQXBJHp/CNK1OjFpvorQ2zAcjZ2tjUycpTmQnqJgyZBqN4jYFxWW5bF5CeZwgBXLsbiHlePpjKRHnXq1dGaGyhiwmFa4D1h2flttrqVkak1uIokSZYAkBnNy4s7ZQnKy9YPrVzUVFeHYiws4S6t21JAtXSzICdYS4Ya2TA0Yg/pGvFviNh7pz5sNiToTdADAwfDcy5TICwHVX836VbsTYUc8hHgAOxCoZ6G2oMH0YEeYmKrnFcNZuW271AbSzF1ytsCW2SA7K065NQ30RtWbM+Lu/W+7XAuypuGF3M+dRBBF22JO50bOpkRrIqu9plwVtSLltO91i0gAdN4zkQimdfASR5SDVXHHbuH7y3h77i1qFzaEA9UJ1tkjTpIJ2mB67NYWxeuHvnXMDKozFO8JEwznSP0RBadzXO9a14428Nh8Zi7Zgvkt5vFdzW2YQGW2WJEeuYqIPMNBUzwjjb4IKl7DOGUQtxSSWIEZ8r+PKDAIMAaCBtYsHhCDDs6BCVAtFLaqq6DOVEjw7JsAYJO25dwzMhDC3dQjVSsW25ojvmGUmOjoSTIzVucpekdwrtJhmIW2yEnSHJS4R4iSt1u7ZpnUOSSZ6k1ZPyoETqsCTn0iRIgkBV89THv2qkY/s5hbjFLbd1d0+TfKVJJ0gZyGnoEeNSchqvWeI3LBe1bvxlJBWS1ssDOYZokg9SBBBgGJperPpkvxbOI8UfE3Gs2x8hmyOCp+WcGckTpbTMM5+cSqgZ3Aq0LhSfk/CCV7yGKkBI7uwhWdLYC5iCAx0k8wFC7H8dtWGPelhcbMVcksssOUQASkHMfCxJeSvKsdGwD28o7srBmIAGbKYOu7n1Op+NOfadNDiPBO9TKtxlO66yFYNmD6fKBv0lcdfM1hNzEWQvfWxeRQPlLWjrBzt8meZpg8qFtlGQb1PvcA3+3T7BWpbvtdYrZ5oMM8HIhA+kNXYGOVTInUit3Ea+Gwa3ES9aO4BB8pAaJHw1Gh0qSu8YZT4OuuY/aI/GK3sPhVtqYO+rMesCJPQAAbDSojit7NmREzkCYkAAEaMX1FtdZkyxgwjb1kbVvj9sg6GViVEltWyqAANSTsP71J2cRmUEqVJ+a0SPQ5SRPuJqu8HsIIYsrEnMmUEKAwlSoMlnyQC5LE6xlBiprNVwbvejzr7nHnWgXrG94AEnYbnoPiamCVBr7VUxfaK1bHKTcP/DgruBHeMRbBl10zTzVX+J9rLrgcws253zHvCB1zMBkkyOVLnmI3pg6XSqd2S7Z28UxtMecaBwCEcjdQTs+s5dyJMaGrjUClKUHyuW+19w7WrU7Iz+4luU/WldNv3MqlvIf+Kq/HOEWsYkXOW4ByuNx6EdV9KluLIonYztPnjD3jF5RlWW0uKAxy53aFj5sDyHlN6tP1B02kaDQeC40a/3rlPaXsdiLMl0JQeG5bkgeWvT3GK2eDdsr1qExQ7zoL7S2h2DLs3XybyzVudJjol/h1u4dFhlgZrQC3LczPym5TfT/AMVHYvgpLqwCORJARQLhH0kuKuUN+yDpuSaz8O41avjNbfMAT42UG3oD4Jlk+HSCKkkuSIgsInInKh18SsY19PfVFQx3DLRKtc8TGJcm3eXWFDXFbmMRrca5t4F6a3EeCXZXvCt0wAnfyt2NwLeJtAOwA6NbyDrV6LKdJGunKPF+i5/H1JqMxlm2im2kqX3tIc7OfRWORvWdhPvrPiuqp/tzEYdD3y3LtoQCt0ZiBI5VxNgm3ciPC+SPI9Z3hXF1vWs1lbipsFIYLm3OVsvMfRYUa77Vlw6PIzEOyyBkzKgAI8T5jmbzFqEBBUk9av2jXG3XexayMighksvEAaqLrsFCkzpb3joYms7Yv1s8a7U27WYArdubMFJFsHoXuA87DUZV3O7edSe9jMdcjVyNQoGVLak75Ry219+p99bfDuyl1mzYgNZRddVhjEZsgOiCD+ccx5ZoIFrt4O7bQJh8MTa3LWriT0hpdouPBguSsdABFZ99fV9T4w8A4Baw7SQb+IABbIjt3YYGCEVWZAYIzlST0AE1n4p2TsXSxtqqXeclAy5TB5mBSRA01VZXNLoSa2U4xbUBHwuLtAHNNtHZZJksWskoTE75j51lfjuCblbFLa1DZSVtST85lVQcwgQ0g+6Nd5Mxn2r2F49fwbC3eU3EWIZvGikyAHkyk6SCVOoMnlF24dxC3iENy0wYxzAoz5dB4kJyr1/dMEmRULxG5hsVbFt7tu40H5a3kjNG5CZueNJ5VcaEaCue/lF/BX3CuUYZkJRjEONCCp8LAgjy0nUVnby1mug9pO0jW81iw6s0/KNccKqQfApLBRAA0EZYgS0xp9n+Di7/AO6xNm3B5bVu3bTO7FJUajmbKJAaVRASRJGSvdmOH27tzvbgbukOoPMHcc0ZQNFAgwZDEgajMtdAZ+8IcsUTLkFqS2VCc0nXmcnKWJzSQPKac7fdL69RXOJdmEJY4dTKgtdwzlVdFMQVYkhRrOsprrkgLUJgOJ3cK57tmIBhrbgrr5MrTDDyOuujCYroK3MMuUtcuFk8BzlckdQtuIIB6gyCZ3NRnaXgK39QptX1HI5UhbywWy5VBk+gkDdfnW6t5/El/WzwLjmHxhVbgYMN7QYgXNh0/OdSUIEQdwJq6tjbdtNICgQAIVREACTAU66AkT0mvz+y3EciGS6rQRMMCNRzDrsQwOuhnY10Dsl2gS+Ut3o71PzbbAwpBgIBD6CQNCBptFSdfq3ldDi3uRkJUESGUeQPhBELuPzmvLog0NYrWDa2p7p2U+Jply7EczPmYFm66MBPStpPLT4ADf0B/wA+Feyf8Mfd0rrjDQxGNaMt61mXN4rOdgApEBgAGAImRlyxpJ3r0uIR1LWrspzB1knUnQK2bkMr5HQkaHWtx9d/PqfwH3GtDFYJGhioDKJVvCFIMgwuv4dYpgg8PjSbptG5dU5XGUtmDJlYqQ1xgXiZMajKoJ2Lbps2dMzM5UaZlIzZQikzdzNBKHUGOfqda1cdgMwKXedJJFy4pB5RmSI0aIHMIMsDptVZvY+9aMZu+tz4LrEnoARc1LbAw4aco10rIhu0XaTFreFpowtrOqC6LbswSVDOLjks4AUNlXLOUaA1GtaUgvbtvdUzGIxZhD5lEOj7HQByJ3q0PxGzdBUO9pmmbd2CG1kgA6OTJAAIAAE+VV7iHCwnObagCYZQXsyIUlre2UHlzLCzIGY0C9i2HdsrtoqlGjJ+0g+amYHLHSK652B7UNjFupcy94gRgQIzK4IkjzDKw92WuMPbv3WIW291yJ+SVrmYbZgFEx0ggEdQKtvs2F2zxO3buq1tnsOrIwIaJW4hKnY6HfWlHa6UpUER2kxK28O7s6oAVlmICjmA1J6VD4fHacw+I1FffaVh3fAuEBJDISB5THT1IrkScYvC3aXM691KjuyytGg5tYYiIgjYRXLvrK6cc7HarWIB2YVH47s3g7xJewmY7skqTPmF0b4g1zrA9sbgibqXP+bbCt7s1sqPsNWrhXaQXBusiTlt3CzEDUsFZVzaScq5m0OhrM7av+decR7OrIYXMLfu4dx4dA6j3LykaaQDGu1S2B4NilTLcuWrpBGoL2wQOuVVOVvcSPdW9h8SWUMrhlYAggqQQdiD5VmW8/of89K3O2fFqXuHXoPylsEzrDZY/SUDm/eX31hscBAnNiGYHTlXKSJmCxZuWfmiFPUMZJlFxcbgivedG8p89j/enlqY0W4TZiD3pER4ypiI0a3lK+WhFfcNwvDW0Fu3ZVUEwoLxqZPXWevn1rPjEcITbMnTTrEiY8zE1HLjzIB39fPqNI19Kmq2cRw3DuIa2286XLok+ZhuYj1mtS5wCyfDcuofUo4+pln4gg+tbIuz6e7+9ew59/8AnoKvtPSOv8JxAK93eFxRuGJVvOFUyp8tWHvrVc4hNbiuqAagqpY80QMhKERqDmJ8x0qaGKHUEeu4+sbfGti3f0kGR5gyKeRin45lWycQLSXbg0ti3bBMMYVmKcyypnfRZOpYCuaMl2+91yGuHK1y40Gco1a4fJBpHT7K7fieGYe7mz2klgQxWUZgdCCyEEg+Rr3/ALOSIW7dVYAC5gywPCIYbabTEabUt1Z6cZ7McQbD3IdgLVyFeHSYmVcrJK5T1I2LRXSMDaiR3duF0Lxce4cxkDukOXKIInNpAA3rexPZDC3BrasH/wCs2z+9bYfdWexwS5aZMg0WFhLmpTYgtc1bo2snlOusjXN/WbGS1hnBguLcsRFtVGcdTNuGtz6sd/Svn+zLZEFFzEEy2Vrk9Ct75hESCNoreW066ZCJ37teQ/rQJO1fcpiCDH0YYIdfMwZ/GuuxnFM7WcA74ZkA/KLajlGX5S3oMsKACRO+wZhsH5KDYt3Cc1tHYASWUNygahiR4SI3Mbe6u1Yiw7jKq5isFVYEIvKVOp/OKQSpXqCffUNg+zll3a6+ClnIabqkySJJKOcqtO5CqSZJ11PPrmNysfY3tEMQjK7TctwHIAjnzEOSdFzEQVA0InZoWzLiARodY2BWfr2P+eVatzh7MmQWsg+bBQBY2gTArXw/BboADXFIBmCzmDBUwBplMkxpB11MRZ1kZsbN7iKAkZpMnRAzHTTXy3AkTvULjuOmOQ93pvoXB5iRPzdVTzkMdNKkz2dUznutqZIRQNdBuZ6ADboPKsqcBwq6shfr8q7H7NB9lL2YpuL4i1wFLYZmZiy5cz3NWLEE7mC5gbDoNqx4fs1jbp1ti2D1unL/AAiXB/ZroNgqoy2rYA8raqq/WIr0bbHdgP1f6jWL214qfb7CWRriLzXJ+agVVPpLZmPwy1LYLs/hrQ+Tw513N13eY2OW4zeZ2XqamBlXYa+e5+s6/VFRnGeM2sPb7y9cCL0HznPkq/OP+Ega1NtXIyY/GW7NtnuMFRBJA0RQNAMo39N9SNBUB7N+F3b+Ku8VuqVW4GSwp3ZSQDc/VCqFHnr6E6nDOFYjjFy3dvL3XD1JZLebnvFSV5o1A3k7AaLJJaurWraqoVQFUAAACAANAAOgrXMxnqstKUrTKO45aD2HUiQYB90iap2J7OYYjOLZD6/KBmz6jKTLGDppDT91XXiuHe5ZuW7b5HZGCPAOViOVoO8GK5Piu0nE8GSMZg0ZNs65kB6TnGdDPkAN+m1Y651vnrEs/Yuy3+qx/WS0ftyisP8A6DtghkuKjAgg92ZBGoIyXBrWtgPaHgzpcW7a/WTOv1oS38Iqdw3anAuJXF2B+u4tn925lP2Vz8J+N/0v6lOD8Na1b7s3BcOZmnL3fi1IA16yZncmth+U8wn6/wAa07XF8OdVv2SPS5bP3NUhaxCXBEhvIg5vuNaxLdFFs9CPcRXlsOvRiPeJ+6tcMA5AIJEZhOoB2MVsBSdgT7gaI+AOu0N7jP2VhxNlLoOwf16+h/nuPsrYyv8ARb4g15dT85T74P4/2qCKs3CpyNvMCfPfKfXrPUEGsz2Mw9Rtv+HSsPFb1tIe46hTyMSwHmVOp3B0/a9Kq3G+29u0sWnS7+krCR7/AOdbnxL9W0P5/wCfWTpXlmWZBhvMGD/f4zXPOAjiXEi7WXt2ranx3S4Uk7hCqmTpr02qwWuwWP3fiNpfQIzD7SKZqLMmLYaGG9QNfivX3j6q2bbltVyt7jUHY7IYy2JGMtXG6ZrbKB/E1aLcTexe7nEhluDVXQMUcH6LAa+vl5CpeWvJcBm+ifrH4xX1bjfRb4f2NRCcWCrmcysTKjUD1Ub/AAA+NTGZuuaPjFTLDY9C630X+r+1exiWHRv3TWs+LRfE6L+syj768JxO0TlW9aLHYB7ZJ8uUGTQbwxR+i37pr7+UP0RviI++tF+I21LKzorIpdlJUFVAksQdYHnUNf7b4Bd8Qp/Ut3G+1bcVfZ6WZr1z6H4/dWI33Omg99VRvaBgRtcdvdacf9QWtW77TcKNBaxDfs2wPtuT9dTKbF4yE7v8FEfaa+ZEHST5nU/bp99VOx26wThSrXCxEsi2rjMhiYbKpB8uUkfDWoHH9v8AFlslrBZCfD3od2IOxyoFg/tEVcqa6U97/D/kVHcU4xZsDNeupbHTO2p9y+JvgDXOu94zifHc7hD5FLcDzm2Dc+s1J8K7B4JWz4vE3LznVggKhj+k7S7e+Vq+NPJI3O0WKxSMeHYO5dAn5a6FS3I0ORXYG4empEEag167P+zq5dcYnilw3bmhFqZURsHI0j9FYXfeauWB4nhLVtLds5UQBVUAwAK2l45hz/qD6jWpMZt1v27YUBVAAAgACAANgANhWStO3xK021xTWyrg7GaqPdKUoPlRmN4T3k/LXkn6LiPqINSlKDnvEfZfYutmN5w285EmfPlAmo1/ZCOmL+u0P666pSg5E/sdbpi0+Nn/APdYH9jdzpibXvNpp/6q7JSg5CfZVjFfvLePK3SILg3AzACACwaTsK1sR7KMbcOa5iLVx/p3DcZj5asCftrs9KDiP+5/FfTw38f9NB7H8Tr8ph/qbT+Gu3UoOLL7Hr//AM1lfcjVtWvY+3zsQs+iH+quv0oOfcG7A3sPouMOX6OQx/1aVPL2bfriH+C/3qx0oK23Zp+mJcfsj+dQnaT2fPi7YRsVqpzIWtzB2+l5Vf6UHGD7Hb42xNk+pRh+Jrz/ALm70/n7EeWRv5V2mlBxgex++NruH/dcfhXy57I8UdO+sR6Ztf4K7RSg4yvsivgybllj5kuTptutH9lOLJ/OWY/Wf+iuzUoOMJ7KMUIGezA/Sf8AprMvsrxH07HxZz/212GlBzW32Fxo/wBaz7uf+mthOxeM63rQ92f+VdCpQUW32Jv/ADsSvwQn8RWxb7E/Svk+5Y/7quVKCsL2Ot9btw/u/wAq2LXZXDjfO3vb+QFT9KCPw/CLCeG2PjJ++t1UA2EV7pQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQKUpQf/9k="));
            _sko.Add(new Sko(3, "Adidas", "Campus", 42, 700, "https://assets.adidas.com/images/h_840,f_auto,q_auto,fl_lossy,c_fill,g_auto/ce738cbe5342421996feaf5001044964_9366/Campus_00s_sko_Gra_HQ8707_01_standard.jpg"));
            _sko.Add(new Sko(4, "Asics", "Gel-Kayano", 44, 600, "https://images.asics.com/is/image/asics/1201A019_108_SR_RT_GLB?$zoom$"));
            _sko.Add(new Sko(5, "SneakerCare", "SneakerCare", 0, 49, "https://m.media-amazon.com/images/I/71oI5Pyxc2L._AC_UY900_.jpg"));
            }

            public List<Sko> GetAll()
            {
                return new List<Sko>(_sko);
            }

            public Sko GetById(int skoid)
            {
                Sko? sko = _sko.Find(s => s.SkoId == skoid);
                if (sko is null)
                {
                    throw new KeyNotFoundException();
                }
                return sko;
            }
            public Sko Add(Sko sko)
            {
                _sko.Add(sko);
                return sko;
            }

            public Sko Delete(int skoid)
            {
                Sko deleteSko = GetById(skoid);

                _sko.Remove(deleteSko);
                return deleteSko;
            }

            public Sko Update(int skoid, Sko updatedSko)
            {
                if (skoid != updatedSko.SkoId)
                {
                    throw new ArgumentException("kan ikke opdatere id og obj.Id er forskellige");
                }

                Sko updateThisSko = GetById(skoid);

                updateThisSko.Maerke = updatedSko.Maerke;
                updateThisSko.Model = updatedSko.Model;
                updateThisSko.Str = updatedSko.Str;
                updateThisSko.Pris = updatedSko.Pris;

                return updateThisSko;
            }

            public Sko ReadSko(SqlDataReader reader)
            {
                throw new NotImplementedException();
            }

        
           
    }
