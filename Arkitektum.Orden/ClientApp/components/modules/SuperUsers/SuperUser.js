export default {
    name: 'SuperUser',
    props: ['apiUrl', 'superUser', 'saved'],
    data() {
        return {
            editable: false,
            data: this.superUser !== undefined ? this.superUser : {name: '', email: ''}
        }
    },
    methods: {
        emailAsLink: function(email) {
            return 'mailto:' + email;
        }
    }
}