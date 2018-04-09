export default {
    name: 'ResourceLink',
    props: ['apiUrl', 'resourceLink', 'saved'],
    data() {
        return {
            editable: false,
            data: this.resourceLink !== undefined ? this.resourceLink : {description: '', url: ''}
        }
    }
}
