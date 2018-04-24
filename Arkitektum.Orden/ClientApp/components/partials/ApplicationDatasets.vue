<template>
    <div>
        <div class="list-group list-group-flush">
            <!-- Saved application datasets -->
            <application-dataset v-bind:application-dataset="applicationDataset" v-bind:saved="true" v-on:remove="removeApplicationDataset(applicationDataset.id)" v-for="applicationDataset in apiData" :key="applicationDataset.id"></application-dataset>

            <!-- New application datasets -->
            <application-dataset v-if="newApplicationDataset" v-bind:selected-application-datasets="apiData" v-bind:application-id="applicationId" v-bind:saved="false" v-on:remove="removeNewApplicationDataset()"></application-dataset>

            <div class="list-group-item" v-if="writeAccess">
                <button v-show="!newApplicationDataset" v-on:click="createNewApplicationDataset()" class="float-right btn btn-outline-primary"><span class="fas fa-plus"></span> Legg til datasett</button>
            </div>
        </div>
    </div>
</template>

<script src="./ApplicationDatasets.js"></script>
<style src="./ApplicationDatasets.scss"></style>
