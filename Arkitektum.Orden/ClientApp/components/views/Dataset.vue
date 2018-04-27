<template>
    <div>
        <h1>{{ apiData.name }}</h1>
        <hr/>
        <div class="row">
            <div class="col-lg-8">
                <div class="btn-group" v-if="writeAccess">
                    <a v-bind:href="apiUrls.edit" class="btn btn-sm btn-outline-primary"><i class="fas fa-edit"></i>
                        Rediger</a>
                </div>

                <p v-if="apiData.userModified">Sist endret av {{ apiData.userModified }} - {{ apiData.dateModified |
                    formatDate }}</p>
                <p v-if="apiData.userCreated">Opprettet av: {{ apiData.userCreated }} - {{ apiData.dateCreated |
                    formatDate }}</p>

                <p>Inneholder personopplysninger: <strong>{{ apiData.hasPersonalData | formatBoolean }}</strong></p>
                <p>Inneholder sensitive personopplysninger: <strong>{{ apiData.hasSensitivePersonalData | formatBoolean }}</strong></p>
                <p>Inneholder master data: <strong>{{ apiData.hasMasterData | formatBoolean }}</strong></p>
                
                <h2>Beskrivelse</h2>
                <hr/>
                <p v-if="apiData.description" class="long-text">{{ apiData.description }}</p>
                <p v-if="!apiData.description"><i>Ingen beskrivelse angitt.</i></p>

                <h2>Formål</h2>
                <hr/>
                <p v-if="apiData.purpose" class="long-text">{{ apiData.purpose }}</p>
                <p v-if="!apiData.purpose"><i>Ingen formål angitt.</i></p>

                <h2>Drift</h2>
                <hr/>
                <p v-if="apiData.hostingLocationText">Driftsplassering: {{ apiData.hostingLocationText }}</p>
                
                <h3>Informasjonselementer</h3>
                <hr/>

                <dataset-fields v-bind:dataset-id="datasetId" v-bind:dataset-fields="apiData.fields" v-bind:write-access="writeAccess"></dataset-fields>
            </div>
            <div class="col-lg-4">
                <div class="card">
                    <div class="card-header">Applikasjoner</div>
                    <dataset-applications v-bind:dataset-id="datasetId" v-bind:dataset-applications="apiData.applications" v-bind:write-access="writeAccess"></dataset-applications>
                </div>
            </div>
        </div>
    </div>
</template>


<script src="./Dataset.js"></script>
<style src="./Dataset.scss"></style>
