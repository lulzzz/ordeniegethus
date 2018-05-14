export default {
    name: 'ResourceLink',
    props: ['resourceLink', 'saved'],
    props: {
        resourceLink: Object,
        saved: Boolean,
        writeAccess: {
            type: Boolean,
            default: false
        }
    },
    data() {
        return {
            editable: false,
            data: this.resourceLink !== undefined ? this.resourceLink : {description: '', url: ''}
        }
    },
    methods: {
        update() {
            
            this.$parent.updateResourceLink(this.resourceLink.id, this.data);
            
            this.editable = false;
        }
    }
}
