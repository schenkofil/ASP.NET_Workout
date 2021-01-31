$.validator.addMethod('filecontent', function (value, element, params) {
    var filecontentType = params[0].accept.replace("*", "");

    var fileContentTypeFromFile = "";
    if (element && element.files && element.files.length > 0) {
        fileContentTypeFromFile = element.files[0].type;
    }

    if (!value || fileContentTypeFromFile && fileContentTypeFromFile != "" && fileContentTypeFromFile.toLowerCase().includes(filecontentType)) {
        return true;
    }

    return false;
});

$.validator.unobtrusive.adapters.add('filecontent', ['type'], function (options) {
    var element = $(options.form).find('#file')[0];

    options.rules['filecontent'] = [element, parseInt(options.params['type'])];
    options.messages['filecontent'] = options.message;
});