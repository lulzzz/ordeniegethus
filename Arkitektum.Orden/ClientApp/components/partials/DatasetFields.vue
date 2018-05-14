<template>
    <div>
        <div class="list-group list-group-flush">
            <!-- Saved dataset field -->
            <dataset-field v-bind:dataset-field="datasetField"  v-bind:write-access="writeAccess" v-bind:saved="true" v-on:remove="removeDatasetField(datasetField.id)" v-for="datasetField in apiData" :key="datasetField.id"></dataset-field>
            
            <!-- New dataset field -->
            <dataset-field v-if="newDatasetField" v-bind:selected-dataset-fields="apiData" v-bind:dataset-id="datasetId" v-bind:saved="false" v-on:remove="removeNewDatasetField()"></dataset-field>

            <div class="list-group-item" v-if="writeAccess">
                <button v-show="!newDatasetField" v-on:click="createNewDatasetField()" class="float-right btn btn-outline-primary"><span class="fas fa-plus"></span> Legg til informasjonselement</button>
            </div>
        </div>
    </div>
</template>

<script src="./DatasetFields.js"></script>
<style src="./DatasetFields.scss"></style>
