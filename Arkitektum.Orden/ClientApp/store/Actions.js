import axios from 'axios';

export default {
    getApiData: function (context, payload) {
        axios.get(payload.url).then(response => {
            context.commit('setApiData', response.data)
        });
    }
}
