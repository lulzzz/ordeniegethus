export default {
    name: 'ResourceLink',
    props: ['apiUrl', 'resourceLink', 'saved'],
    data() {
        return {
            editable: false,
            data: this.resourceLink !== undefined ? this.resourceLink : {description: '', url: ''}
        }
    },
    methods: {
        update: function() {
            this.$parent.updateResourceLink(this.resourceLink.id, this.data);
            this.editable = false;
        }
    }
}
