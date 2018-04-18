<template>
    <div v-if="apiData">
        <h1>{{ apiData.name }}  - versjon {{ apiData.version }}</h1>
        <hr/>
        <div class="row">
          <div class="col-lg-8">
              <p>
                <div class="btn-group">
                  <a v-bind:href="apiUrls.edit" class="btn btn-sm btn-outline-primary"><i class="fas fa-edit"></i> Rediger</a>
                  <a v-bind:href="apiUrls.submitAppRegistry" class="btn btn-sm btn-outline-primary"><i class="fas fa-warehouse"></i> Send inn til felles applikasjonsregister</a>
                </div>
              </p>
                
              <p v-if="apiData.userModified">Sist endret av {{ apiData.userModified }} - {{ apiData.dateModified | formatDate }}</p>
              <p v-if="apiData.userCreated">Opprettet av: {{ apiData.userCreated }} - {{ apiData.dateCreated | formatDate }}</p>
              <p>Leverandør: {{ apiData.vendor.name }}</p>
              <p>Systemansvarlig: <span class="badge badge-info"><i class="fas fa-user"></i> {{ apiData.systemOwner }}</span></p>

              <h2>Kostnader</h2>
              <hr />
              <p>{{apidata}}</p>
              <p>Årlige kostnad: {{ apiData.annualFee }}</p>
              <p>Innkjøpskostnad: {{ apiData.initialCost }}</p>
              
              <h2>Drift</h2>
              <hr />
              <p>Driftsleverandør: {{ apiData.hostingVendor }}</p>
              <p>Driftsplassering: {{ apiData.hostingLocationText }}</p>
              <p>Antall brukere: {{ apiData.numberOfUsers }}</p>

              <p>
                Superbrukere:
                <a href="" class="badge badge-info">
                  <i class="fas fa-user"></i>Hans Hansen
                </a>
                <a href="" class="badge badge-info">
                  <i class="fas fa-user"></i>Jan Johansen
                </a>
                <a href="" class="badge badge-info">
                  <i class="fas fa-user"></i>Ole Olsen
                </a>
              </p>

              <h2>Lenker</h2>
                <hr />
                <resource-links v-bind:application-id="applicationId"></resource-links>

<!--                
                <h2>Avtale</h2>
                <hr />
                <p>Detaljer om avtale.</p>

                
                <h2>Vurderingskriterier</h2>
                <hr />
                <p>Brukertilfredshet:</p>
                <p>Applikasjonsmodenhet:</p>
                <p>Skalerbarhet:</p>
-->
            </div>
            <div class="col-lg-4">
                <div class="card">
                    <div class="card-header bg-info text-white">Tjenesteområder</div>
                    <div class="card-body">
                        <application-sectors v-bind:application-sectors="apiData.sectors" v-bind:applicationId="applicationId"></application-sectors>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script src="./Application.js"></script>
<style src="./Application.scss"></style>
