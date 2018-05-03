<template>

    <!-- Saved dataset field-->
    <div v-if="saved" class="list-group-item">
        <div v-show="editable">
            <div class="form-group row">
                <label v-bind:for="'dataset-field-name-' + datasetField.id" class="col-sm-2 col-form-label">Navn</label>
                <div class="col-sm-10">
                    <input v-model="data.name" v-bind:id="'dataset-field-name-' + datasetField.id" placeholder="Navn" class="form-control" />
                </div>
            </div>
            <div class="form-group row">
                <label v-bind:for="'dataset-field-description-' + datasetField.id" class="col-sm-2 col-form-label">Beskrivelse</label>
                <div class="col-sm-10">
                    <input v-model="data.description" v-bind:id="'dataset-field-description-' + datasetField.id" placeholder="Beskrivelse" class="form-control" />
                </div>
            </div>
            <div class="form-group">
                <div class="custom-control custom-checkbox my-1 mr-sm-2">
                    <input v-model="data.isPersonalData" type="checkbox" v-bind:id="'dataset-field-is-personal-data-' + datasetField.id" placeholder="Beskrivelse" class="custom-control-input" />
                    <label v-bind:for="'dataset-field-is-personal-data-' + datasetField.id" class="custom-control-label">Er personopplysning</label>
                </div>
            </div>
            <div class="form-group">
                <div class="custom-control custom-checkbox my-1 mr-sm-2">
                    <input v-model="data.isSensitivePersonalData" type="checkbox" v-bind:id="'dataset-field-is-sensitive-personal-data-' + datasetField.id" placeholder="Beskrivelse" class="custom-control-input" />
                    <label v-bind:for="'dataset-field-is-sensitive-personal-data-' + datasetField.id" class="custom-control-label">Er sensitive personopplysning</label>
                </div>
            </div>
            <div class="float-right">
                <span class="btn btn-outline-secondary" v-on:click="editable = !editable"><span class="fas fa-times"></span> Avbryt</span>
                <span class="btn btn-outline-success" v-on:click="update()"><span class="fas fa-check"></span> Oppdater</span>
            </div>
        </div>
        <div v-show="!editable" v-bind:class="{'justify-content-between align-items-center': !editable}">
            <div class="row">
                <div class="col-sm-4">
                    <span data-toggle="tooltip" 
                          data-placement="bottom" 
                          v-bind:data-original-title="[datasetField.isPersonalData ? 'Elementet har personopplysninger' : 'Elementet har ikke personopplysninger']"  
                          class="field-icon" 
                          v-bind:class="{disabled: !datasetField.isPersonalData}">
                        <span class="fas fa-shield-alt"></span>
                    </span>
                    <span data-toggle="tooltip" 
                          data-placement="bottom" 
                          v-bind:data-original-title="[datasetField.isSensitivePersonalData ? 'Elementet har sensitive personopplysninger' : 'Elementet har ikke sensitive personopplysninger']"
                          class="field-icon"
                          v-bind:class="{disabled: !datasetField.isSensitivePersonalData}">
                        <span class="fas fa-lock"></span>
                    </span>
                    {{ datasetField.name }} 
                </div>
                <div class="col-sm-5">{{ datasetField.description}}</div>
                <div class="col-sm-3 text-right" v-if="writeAccess">
                    <span class="btn btn-outline-secondary" v-on:click="editable = !editable"><span class="fas fa-edit"></span></span>
                    <span class="btn btn-outline-danger" v-on:click="$emit('remove')"><span class="fas fa-trash"></span></span>
                </div>
            </div>
        </div>
    </div>

    <!-- New dataset field -->
    <div v-else class="list-group-item">
        <div class="form-group row">
            <label for="dataset-field-name-new" class="col-sm-2 col-form-label">Navn</label>
            <div class="col-sm-10">
                <input v-model="data.name" id="dataset-field-name-new" placeholder="Navn" class="form-control" />
            </div>
        </div>
        <div class="form-group row">
            <label for="dataset-field-description-new" class="col-sm-2 col-form-label">Beskrivelse</label>
            <div class="col-sm-10">
                <input v-model="data.description" id="dataset-field-description-new" placeholder="Beskrivelse" class="form-control" />
            </div>
        </div>
        <div class="form-group">
            <div class="custom-control custom-checkbox my-1 mr-sm-2">
                    <input v-model="data.isPersonalData" type="checkbox" id="dataset-field-is-personal-data-new" placeholder="Beskrivelse" class="custom-control-input" />
                <label for="dataset-field-is-personal-data-new" class="custom-control-label">Har personopplysninger</label>
            </div>
        </div>
        <div class="form-group">
            <div class="custom-control custom-checkbox my-1 mr-sm-2">
                    <input v-model="data.isSensitivePersonalData" type="checkbox" id="dataset-field-is-sensitive-personal-data-new" placeholder="Beskrivelse" class="custom-control-input" />
                <label for="dataset-field-is-sensitive-personal-data-new" class="custom-control-label">Har sensitive personopplysninger</label>
            </div>
        </div>
        <div class="float-right">
            <span class="btn btn-outline-danger" v-on:click="$emit('remove')"><span class="fas fa-trash"></span> Fjern</span>
            <span class="btn btn-outline-success" v-on:click="$parent.postDatasetField(data)"><span class="fas fa-check"></span> Legg til</span>
        </div>
    </div>

</template>

<script src="./DatasetField"></script>
<style lang="scss" src="./DatasetField.scss"></style>
