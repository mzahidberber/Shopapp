/*
 Copyright (c) 2007-2019, CKSource - Frederico Knabben. All rights reserved.
 For licensing, see LICENSE.html or https://ckeditor.com/sales/license/ckfinder
 */

var config = {
    resourceTypes: [
        // Images resource type
        {
            name: 'Images',
            url: '~/img',
            allowedExtensions: ['jpg', 'jpeg', 'png', 'gif'],
            directory: 'images'
        },

        // Other resource types...
    ]
};

// Set your configuration options below.

// Examples:
// config.language = 'pl';
// config.skin = 'jquery-mobile';

CKFinder.define( config );
