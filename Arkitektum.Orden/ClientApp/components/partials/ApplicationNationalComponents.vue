<template>
    <div>
        <div class="list-group list-group-flush">
            <!-- Saved application national components -->
            <application-national-component v-bind:application-national-component="applicationNationalComponent" v-bind:saved="true" v-on:remove="removeApplicationNationalComponent(applicationNationalComponent.id)" v-for="applicationNationalComponent in apiData" :key="applicationNationalComponent.id"></application-national-component>

            <!-- New application national components -->
            <application-national-component v-if="newApplicationNationalComponent" v-bind:application-id="applicationId" v-bind:saved="false" v-on:remove="removeNewApplicationNationalComponent()"></application-national-component>

            <div class="list-group-item">
                <button v-show="!newApplicationNationalComponent" v-on:click="createNewApplicationNationalComponent()" class="float-right btn btn-primary"><span class="fas fa-plus"></span> Legg til tjenesteområde</button>
            </div>
        </div>
    </div>
</template>

<script src="./ApplicationNationalComponents.js"></script>
<style src="./ApplicationNationalComponents.scss"></style>
