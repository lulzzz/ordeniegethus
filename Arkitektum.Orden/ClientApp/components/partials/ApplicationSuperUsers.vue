<template>
    <div>
        <div class="list-group list-group-flush">
            <!-- Saved application super users -->
            <application-super-user v-bind:application-super-user="applicationSuperUser" v-bind:write-access="writeAccess" v-bind:saved="true" v-on:remove="removeApplicationSuperUser(applicationSuperUser.id)" v-for="applicationSuperUser in apiData" :key="applicationSuperUser.id"></application-super-user>

            <!-- New application super user -->
            <application-super-user v-if="newApplicationSuperUser" v-bind:available-super-users="availableSuperUsers" v-bind:saved="false" v-on:remove="removeNewApplicationSuperUser()"></application-super-user>

            <button v-if="writeAccess" v-show="!newApplicationSuperUser && availableSuperUsers.length" v-on:click="createNewApplicationSuperUser()" class="btn btn-outline-primary list-group-item-button"><span class="fas fa-plus"></span> Legg til superbruker</button>
        </div>
    </div>
</template>

<script src="./ApplicationSuperUsers.js"></script>
<style src="./ApplicationSuperUsers.scss"></style>
