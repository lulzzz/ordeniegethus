<template>
    <div>
        <div class="list-group list-group-flush">
            <!-- Saved application datasets -->
            <application-dataset v-bind:application-dataset="applicationDataset" v-bind:write-access="writeAccess" v-bind:saved="true" v-on:remove="removeApplicationDataset(applicationDataset.id)" v-for="applicationDataset in apiData" :key="applicationDataset.id"></application-dataset>

            <!-- New application datasets -->
            <application-dataset v-if="newApplicationDataset" v-bind:selected-application-datasets="apiData" v-bind:application-id="applicationId" v-bind:saved="false" v-on:remove="removeNewApplicationDataset()"></application-dataset>

            <button v-if="writeAccess" v-show="!newApplicationDataset" v-on:click="createNewApplicationDataset()" class="btn btn-outline-primary list-group-item-button"><span class="fas fa-plus"></span> Legg til datasett</button>
        </div>
    </div>
</template>

<script src="./ApplicationDatasets.js"></script>
<style src="./ApplicationDatasets.scss"></style>
