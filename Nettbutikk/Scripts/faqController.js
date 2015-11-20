
var urlFAQApi = "api/FAQ/";
var urlQuestionApi = "api/Question/";
var urlCategoryApi = "api/Category/";

var urlFAQ = "FAQ/"

var app = angular.module("App", []);

app.controller("FAQCtrl", ["$scope", "$http", function ($scope, $http) {

    showAllQs();
    showCategories();

    $scope.loading = true;
    $scope.showFaqs = false;
    $scope.showQuestions = false;
    $scope.attemptSend = false;
    $scope.loadingCategories = true;
    $scope.showCategories = false;

    /* List */

    function showCategories() {
        $http.get(urlCategoryApi)
            .then(function (response) {
                $scope.categories = response.data;
                $scope.loadingCategories = false;
                $scope.showCategories = true;
            }, function (response) {
                alert(data);
            });
    }

    function showAllQs() {

        $http.get(urlFAQApi)
            .then(function (response) {
                $scope.faqs = response.data;
                $scope.loadingFAQ = false;
                $scope.showFaqs = true;

            }
        );


        $http.get(urlQuestionApi)
            .then(function (response) {
                $scope.pendingQuestions = response.data;
                $scope.loadingQuestions = false;
                $scope.showQuestions = true;
            }
        );


    }



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

    $scope.showCategoryFAQs = function (id) {
        $http.get(urlCategoryApi + id)
            .then(function (response) {
                $scope.faqs = response.data;
            },
             function (response) {
                 alert("error: " + response.status + " " + response.data)
             })
    }

    $scope.faqRateUp = function (faq) {
        ++faq.Score;

        $http.put(urlFAQApi + faq.Id, faq)
        .then(function (response) {

        }, function (response) {
            alert("error: " + response.status + " " + response.data)
        });
    }

    $scope.faqRateDown = function (faq) {
        --faq.Score;

        $http.put(urlFAQApi + faq.Id, faq)
        .then(function (response) {

        }, function (response) {
            alert("error: " + response.status + " " + response.data)
        });
    }

    $scope.answerQuestion = function (question) {
        $scope.handleQuestion = question;

        $scope.showQuestions = false;
        $scope.showAnswerQuestion = true;

    }

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
                alert("error: " + response.status + " " + response.data)
            })

    }

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

    $scope.cancelSave = function () {
        $scope.handleQuestion = null;
        $scope.showQuestions = true;
        $scope.showAnswerQuestion = false;
        displayQuestionlistMsg("Ingen endringer");
    }

    $scope.deleteFAQ = function (faq) {
        $http.delete(urlFAQApi + faq.Id)
            .then(function (response) {
                showAllQs();
            }, function (response) {
                alert("error: " + response.status + " " + response.data)
            })
    }

    $scope.editFAQ = function (faq) {

    }

    $scope.saveFAQ = function (faq) {

    }
}])
