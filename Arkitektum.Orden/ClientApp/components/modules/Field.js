export default {
    name: 'Field',
    props: {
        name: String,
        writeAccess: {
            type: Boolean,
            default: false
        }
    },
    data() {
        return {
            apiData: null,
            url: `/dataset/metadata/${this.datasetId}`
        }
    }
}