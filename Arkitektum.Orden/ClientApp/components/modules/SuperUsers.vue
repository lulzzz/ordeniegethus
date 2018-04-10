<template>
    <div>
        <div class="list-group list-group-flush">
            <!-- Saved super users -->
            <super-user v-bind:api-url="apiUrl" v-bind:super-user="superUser" v-bind:saved="true" v-on:remove="removeSuperUser(superUser.id)" v-for="superUser in savedSuperUsers" :key="superUser.id"></super-user>

            <!-- New super users-->
            <super-user v-if="newSuperUser" v-bind:api-url="apiUrl" v-bind:saved="false" v-on:remove="removeNewSuperUser()"></super-user>
          <div class="list-group-item">
              <button v-show="!newSuperUser" v-on:click="createNewSuperUser()" class="float-right btn btn-primary">Ny superbruker</button>
          </div>
        </div>
    </div>
</template>

<script src="./SuperUsers.js"></script>
<style src="./SuperUsers.scss"></style>
