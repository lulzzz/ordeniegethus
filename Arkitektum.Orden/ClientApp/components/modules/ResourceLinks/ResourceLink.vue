<template>

    <!-- Saved resource link-->
    <div v-if="saved" class="list-group-item">
        <div v-show="editable">
            <div class="form-group row">
                <label v-bind:for="'resource-link-url' + resourceLink.id" class="col-sm-2 col-form-label">URL</label>
                <div class="col-sm-10">
                    <input v-model="data.url" v-bind:id="'resource-link-url' + resourceLink.id" placeholder="URL" class="form-control" />
                </div>
            </div>
            <div class="form-group row">
                <label v-bind:for="'resource-link-description' + resourceLink.id" class="col-sm-2 col-form-label">Beskrivelse</label>
                <div class="col-sm-10">
                    <input v-model="data.description" v-bind:id="'resource-link-description' + resourceLink.id" placeholder="Beskrivelse" class="form-control" />
                </div>
            </div>
            <div class="float-right">
                <span class="btn btn-outline-secondary" v-on:click="editable = !editable"><span class="fas fa-times"></span> Avbryt</span>
                <span class="btn btn-outline-success" v-on:click="$parent.updateResourceLink(resourceLink.id, data)"><span class="fas fa-check"></span> Oppdater</span>
            </div>
        </div>
        <div v-show="!editable" v-bind:class="{'d-flex justify-content-between align-items-center': !editable}">
            <a v-bind:href="resourceLink.url">{{ resourceLink.description}}</a>
            <div class="float-right">
                <span class="btn btn-outline-secondary" v-on:click="editable = !editable" data-toggle="tooltip" ><span class="fas fa-edit"></span></span>
                <span class="btn btn-outline-danger"><span class="fas fa-trash"></span></span>
            </div>
        </div>
    </div>

    <!-- New resource link-->
    <div v-else class="list-group-item">
        <div class="form-group row">
            <label for="resource-link-url-new" class="col-sm-2 col-form-label">URL</label>
            <div class="col-sm-10">
                <input v-model="data.url" id="resource-link-url-new" placeholder="URL" class="form-control" />
            </div>
        </div>
        <div class="form-group row">
            <label for="resource-link-description-new" class="col-sm-2 col-form-label">Beskrivelse</label>
            <div class="col-sm-10">
                <input v-model="data.description" id="resource-link-description-new" placeholder="Beskrivelse" class="form-control" />
            </div>
        </div>
        <div class="float-right">
            <span class="btn btn-outline-danger" v-on:click="$emit('remove')"><span class="fas fa-trash"></span> Fjern</span>
            <span class="btn btn-outline-success" v-on:click="$parent.postResourceLink(data)"><span class="fas fa-check"></span> Legg til</span>
        </div>
    </div>

</template>

<style src="./ResourceLink.scss"></style>
<script src="./ResourceLink"></script>
