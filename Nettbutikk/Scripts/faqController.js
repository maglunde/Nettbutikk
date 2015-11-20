
var urlFAQApi = "api/FAQ/";
var urlQuestionApi = "api/Question/";
var urlCategoryApi = "api/Category/";

var urlFAQ = "FAQ/"

var app = angular.module("App", []);

app.controller("FAQCtrl", ["$scope", "$http", function ($scope, $http) {

    $scope.loading = true;
    $scope.showFaqs = false;
    $scope.showQuestions = false;
    $scope.attemptSend = false;


    /************ FAQs ************/

    // FAQ - get (all)
    $scope.GetAllFAQs = function () {
        $http.get(urlFAQApi)
            .then(function (response) {
                $scope.faqs = response.data;
            }
        );
    }

    // FAQ - post
    $scope.includeQuestion = function (question) {
        $http.post(urlFAQApi, question)
            .then(function (response) {
                $http.delete(urlQuestionApi + question.Id)
                    .then(function (response) {
                        showAllQs();
                        displayQuestionlistMsg("Spørmålet ble inkludert i FAQ");
                    },
                     function (response) {
                         alert("error: " + response.status + " " + response.data)
                     })

            },
            function (response) {
                if (response.status == 400)
                    displayQuestionlistMsg("Feil. Sjekk at spørsmålet er besvart.");
                else
                    alert("error: " + response.status + " " + response.data)
            }
        )
    }

    // FAQ - put
    $scope.putFAQ = function(faq){

        //console.log(faq);return;

        $http.put(urlFAQApi + faq.Id, faq)
        .then(function (response) {

        }, function (response) {
            alert("error: " + response.status + " " + response.data)
        });
    }

    

    // FAQ - delete
    $scope.deleteFAQ = function (faq) {
        $http.delete(urlFAQApi + faq.Id)
            .then(function (response) {
                showAllQs();
            }, function (response) {
                alert("error: " + response.status + " " + response.data)
            })
    }

    /************ PendingQuestions ************/

    // PendingQuestions - get (all)
    $scope.GetAllPendingQuestions = function () {
        $http.get(urlQuestionApi)
            .then(function (response) {
                $scope.pendingQuestions = response.data;
            }
        );
    }

    // PendingQuestion - post
    $scope.sendQuestion = function () {
        var pendingQuestion = {
            Question: $scope.question,
            Email: $scope.email
        };

        $http.post(urlQuestionApi, pendingQuestion)
            .then(function (response) {
                $scope.pendingQuestions = response.data;
                $scope.sendStatusMessage = "Spørsmålet ble sendt";
                $scope.sendStatusClass = "text-success"

                $scope.question = "";
                $scope.email = "";
                $scope.formQuestion.$setPristine();
            }, function (response) {
                $scope.sendStatusMessage = "Feil ved innsending";
                $scope.sendStatusClass = "text-danger"
            });
        $scope.attemptSend = true;
    }

    // PendingQuestion - put
    $scope.saveAnswer = function () {
        $scope.showQuestions = true;
        $scope.showAnswerQuestion = false;

        $http.put(urlQuestionApi + $scope.handleQuestion.Id, $scope.handleQuestion)
            .then(function (response) {
                displayQuestionlistMsg("Svar lagret");
            }, function (response) {
                alert("error: " + response.status + " " + response.data)
            })

    }

    // PendingQuestion - delete
    $scope.deleteQuestion = function (question) {
        $http.delete(urlQuestionApi + question.Id)
            .then(function (response) {
                showAllQs();
                displayQuestionlistMsg("Spørsmål slettet");
            },
             function (response) {
                 alert("error: " + response.status + " " + response.data)
             })

    }



    /************ Categories ************/

    // Categories - get (all)
    $scope.GetAllCategories = function () {
        $http.get(urlCategoryApi)
            .then(function (response) {
                $scope.categories = response.data;
            }, function (response) {
                alert(data);
            });
    }

    // Category - get (list of faqs in )
    $scope.showCategoryFAQs = function (categoryid) {
        $http.get(urlCategoryApi + categoryid)
            .then(function (response) {
                $scope.faqs = response.data;
            },
             function (response) {
                 alert("error: " + response.status + " " + response.data)
             })
    }


    /************ MISC ************/
    $scope.editFAQ = function (faq) {
        faq.editAnswer = faq.Answer;
        faq.edit = true;
    }

    $scope.saveFAQ = function (faq) {
        faq.edit = false;
        faq.Answer = faq.editAnswer;
        $scope.putFAQ(faq);
    }
    
    $scope.cancelSaveFAQ = function(faq)
    {
        faq.edit = false;
    }

    $scope.faqRateUp = function (faq) {
        ++faq.Score;
        $scope.putFAQ(faq);

    }

    $scope.faqRateDown = function (faq) {
        --faq.Score;
        $scope.putFAQ(faq);
    }

    $scope.answerQuestion = function (question) {
        $scope.handleQuestion = question;

        $scope.showQuestions = false;
        $scope.showAnswerQuestion = true;

    }

    $scope.cancelSave = function () {
        $scope.handleQuestion = null;
        $scope.showQuestions = true;
        $scope.showAnswerQuestion = false;
        displayQuestionlistMsg("Ingen endringer");
    }



 


    /************ List ************/

    function showAllQs() {

        $scope.GetAllFAQs();
        $scope.loadingFAQ = false;
        $scope.showFaqs = true;

        $scope.GetAllPendingQuestions();
        $scope.loadingQuestions = false;
        $scope.showQuestions = true;

    }


    /********** Startup **********/

    showAllQs();
    $scope.GetAllCategories();
}])
