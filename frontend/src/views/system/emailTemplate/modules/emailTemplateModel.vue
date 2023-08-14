<template>
  <a-modal
    :title="title"
    :width="800"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleOk"
    @cancel="handleCancel">
    <a-spin :spinning="spinningLoading">
      <a-form-model ref="form" :model="form" :label-col="labelCol" :wrapper-col="wrapperCol" :rules="rules">
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="名称" prop="name" hasFeedback>
              <a-input v-model="form.name" placeholder="" />
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="是否启用" prop="enable">
              <a-switch checkedChildren="启用" v-model="form.enable" unCheckedChildren="禁用" />
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-divider>邮件模板</a-divider>
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item prop="emailHtml" :wrapperCol="{ span: 24 }" hasFeedback>
              <div>
                <a-button type="primary" @click="toggleViewMode">{{ viewMode ? '显示 HTML' : '显示效果' }}</a-button>
                <br /><br />
                <a-textarea v-if="!viewMode" v-model="form.emailHtml" :disabled="viewMode" :autoSize="{ minRows: 4 }" ></a-textarea>
                <div v-else v-html="form.emailHtml"></div>
              </div>
            </a-form-model-item>
          </a-col>
        </a-row>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
export default {
  components: {
  },
  data () {
    return {
      labelCol: {
        md: { span: 6 },
        sm: { span: 4 }
      },
      wrapperCol: {
        md: { span: 14 },
        sm: { span: 20 }
      },
      gutter: 48,
      mdSize: 24,
      smSize: 24,
      formType: 'add',
      title: '添加数据',
      spinningLoading: false,
      visible: false,
      confirmLoading: false,
      viewMode: false,
      rules: {
          name: [{ required: true, message: '请输入名字！' }],
          emailHtml: [{ required: true, message: '请输入邮件模板！' }]
      },
      form: {
        emailHtml: ''
      }
    }
  },
  methods: {
    // 打开页面初始化
    detail (value) {
      this.visible = true
      this.confirmLoading = false
      if (this.$refs.form) {
        this.$refs.form.clearValidate()
      }
      if (value) {
        this.form = {
          ...createForm(),
          ...JSON.parse(JSON.stringify(value))
        }
        this.formType = 'edit'
        this.title = '编辑数据'
        this.quearyEmailHtml()
      } else {
        this.title = '新建数据'
        this.formType = 'add'
        this.form = createForm()
      }
    },
    async handleOk () {
      console.log('handleOk', this.form)
      this.$refs.form.validate(async valid => {
        if (valid) {
          this.confirmLoading = true
          // 修改数据
          if (this.formType === 'edit') {
            await this.$http.put('/sysEmailTemplate', this.form).then(res => {
              if (res.code === 200) {
                this.$message.success('保存成功')
                this.$emit('ok')
                this.visible = false
              }
            }).finally(() => {
              this.confirmLoading = false
            })
          } else {
            // 保存数据
            // 先上传文件
            await this.$http.post('/sysEmailTemplate', this.form).then(res => {
              if (res.code === 200) {
                this.$message.success('保存成功')
                this.$emit('ok')
                this.visible = false
              }
            }).finally(() => {
              this.confirmLoading = false
            })
          }
        }
      })
    },
    toggleViewMode () {
      this.viewMode = !this.viewMode
    },
    async quearyEmailHtml () {
      this.spinningLoading = true
      const res = await this.$http.get('/sysEmailTemplate/getTemplate', { params: { id: this.form.id } }).finally(() => {
        this.spinningLoading = false
      })
      if (res.code === 200) {
        this.form.emailHtml = res.result
        // this.$set(this.form, 'emailHtml', res.result)
      }
    },
    handleCancel () {
      this.close()
    },
    close () {
      this.$emit('close')
      this.visible = false
    }
  }
}
function createForm () {
  return {
    emailHtml: '',
    enable: true
  }
}
</script>
