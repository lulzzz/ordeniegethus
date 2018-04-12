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
        },
        update: function() {
            this.$parent.updateSuperUser(this.superUser.id, this.data);
            this.editable = false;
        }
    }
}