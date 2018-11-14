/*
* vue TinymceComponent
* By Nick
* Ref：https://www.tinymce.com/docs/plugins/  
* 2017-03-15
*/


(function (geo) {
    "use strict";
    
    if (typeof geo === "undefined") {
        geo = {};
    }

    var exports = {
        template: '<div><textarea rows="10"  class="form-control" v-bind:value="editContent"></textarea></div>',
        props: {
            initValue: {
                type: String,
                default: " "
            },
        },
        data: function () {
            return {
                isInit : false,
                uploadPhoto: window.appRoot.rel + 'Photo/UploadTinyMce/',
                editContent:'',
        }
        },
        update: function(newVal, oldVal) {

            console.log('update');
        },
        methods: {
            updateValue: function (value) {
                //console.log(value);
                this.$emit("content-updated", value.trim());
            },
            updateInnerText: function (value) {
                this.$emit("innertxt-updated", value.trim());
            },
            initTinyMce: function () {
                if (!this.isInit) {
                //if (true) {
                    console.log('進到initTinyMce的isInit');
                    var vm = this;
                    //tinymce.remove();
                    //tinymce.editors = [];

                    tinymce.init({
                        target: this.$el.children[0],
                        setup: function (editor) {
                            editor.on("blur",
                                function (e) {                                    
                                    vm.updateValue(editor.getContent());
                                    vm.updateInnerText(editor.getBody().textContent);
                                });
                        },
                        menubar: false,
                        statusbar: true,
                        //image_title: true,
                        automatic_uploads: true,
                        images_upload_url: this.uploadPhoto,
                        file_picker_types: 'image',
                        plugins: ['table code textcolor lists image link'],
                        toolbar1: 'undo redo |  styleselect | bold italic forecolor backcolor ' +
                            '| alignleft aligncenter alignright alignjustify | bullist numlist outdent indent' +
                            ' | code | table | link image ',

                        default_link_target: "_blank",//連結另開
                        link_title: false,

                        anchor_bottom: false, anchor_top: false,

                        file_picker_callback: function (cb, value, meta) {
                            var input = document.createElement('input');
                            input.setAttribute('type', 'file');
                            input.setAttribute('accept', 'image/*');

                            input.onchange = function () {
                                var file = this.files[0];

                                var id = 'blobid' + (new Date()).getTime();
                                var blobCache = tinymce.activeEditor.editorUpload.blobCache;
                                var blobInfo = blobCache.create(id, file);
                                blobCache.add(blobInfo);

                                cb(blobInfo.blobUri(), { title: file.name });
                            };

                            input.click();
                        }
                    });
                    
                    this.isInit = true;
                } else {
                    console.log('進來囉');
                    this.editContent = this.initValue;
                    tinymce.activeEditor.setContent(this.editContent);
                }
            }
            
        },
        watch: {
            'initValue': function () {
                if (this.initValue == null) {
                    this.editContent = ' ';
                }
                this.editContent = this.initValue;
               
                this.initTinyMce();
                
            }
        },
        mounted: function () {
            $(document).on('focusin', function (e) {
                if ($(e.target).closest(".mce-window").length) {
                    e.stopImmediatePropagation();
                }
            });
            //console.log('init-mounted');            
            //this.initTinyMce();                
        }
    };

    geo.geoTinymce = exports;
})(geo);