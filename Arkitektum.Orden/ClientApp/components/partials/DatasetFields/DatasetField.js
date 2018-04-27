export default {
    name: 'DatasetField',
    props: {
        datasetField: Object,
        saved: Boolean,
        writeAccess: {
            type: Boolean,
            default: false
        }
    },
    data() {
        return {
            editable: false,
            data: this.datasetField !== undefined ? this.datasetField : {name: '', description: '', isPersonalData: false, isSensitivePersonalData: false}
        }
    },
    mounted() {
        jQuery(this.$el).find('[data-toggle="tooltip"]').tooltip();
    },
    methods: {
        update() {
            this.$parent.updateDatasetField(this.datasetField.id, this.data);
            this.editable = false;
        }
    }
}
