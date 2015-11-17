
var urlFAQ = "api/FAQ/";
var urlQuestion = "api/Question/";
var urlCategory = "api/Category/";

var app = angular.module("App", []);

app.controller("faqController", function ($scope, $http) {

    showAllQs();
    showCategories();

    $scope.loading = true;
    $scope.showFaqs = false;
    $scope.showQuestions = false;
    $scope.attemptSend = false;
    $scope.loadingCategories = true;
    $scope.showCategories = false;

    function showCategories() {
        $http.get(urlCategory)
            .success(function (data) {
                $scope.categories = data;
                $scope.loadingCategories = false;
                $scope.showCategories = true;
            })
            .error(function (data) {
                alert(data);
            });
    }

    function showAllQs() {

        $http.get(urlFAQ)
            .success(function (data) {
                $scope.faqs = data;
                $scope.loadingFAQ = false;
                $scope.showFaqs = true;

            }
        );


        $http.get(urlQuestion)
            .success(function (data) {
                $scope.questions = data;
                $scope.loadingQuestions = false;
                $scope.showQuestions = true;
            }
        );


    }

    $scope.sendQuestion = function () {
        var userQuestion = {
            Question: $scope.question,
            Email: $scope.email
        };

        $http.post(urlQuestion, userQuestion)
            .success(function (data) {
                $scope.questions = data;
                $scope.sendStatusMessage = "Spørsmålet ble sendt";
                $scope.sendStatusClass = "text-success"

                $scope.question = "";
                $scope.email = "";
                $scope.formQuestion.$setPristine();
            })
            .error(function (data) {
                $scope.sendStatusMessage = "Feil ved innsending";
                $scope.sendStatusClass = "text-danger"
            });
        $scope.attemptSend = true;
    }

    $scope.deleteQuestion = function (question) {
        $http.delete(urlQuestion + question.Id)
            .success(function (data) {
                showAllQs();
            })
            .error(function (data) {
                alert(data)
            })

    }



})